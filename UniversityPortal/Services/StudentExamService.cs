using AutoMapper;
using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;
using UniversityPortal.Services.Base;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace UniversityPortal.Services
{
    public class StudentExamService : BaseService, IStudentExamService
    {
        private readonly IStudentService _studentService;
        private readonly IUniversityService _universityService;
        public StudentExamService(IUnitOfWork unitOfWork,
                                  IMapper mapper,
                                  IDateTimeProvider dateTimeProvider,
                                  ICurrentUserService currentUserService,
                                  IStudentService studentService, IUniversityService universityService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {
            _studentService = studentService;
            _universityService = universityService;
        }


        public async Task<IEnumerable<StudentSemesterViewModel>> GetAllStudentSemester(bool isResult = false)
        {
            var universityId = await _studentService.GetUniversityId();
            var studentId = await _studentService.GetStudentId();
            var studentExam = await UnitOfWork.StudentExamRepository.FindAll(x => x.UniversityId == universityId
                                                                                 && x.StudentId == studentId);

            if (studentExam == null || studentExam.Count() == 0)
            {
                return Enumerable.Empty<StudentSemesterViewModel>();
            }

            if (isResult)
            {
                studentExam = studentExam.Where(x => x.IsResult);
            }

            var semesterExam = await UnitOfWork.SemesterExamRepository.FindAll(x => x.UniversityId == universityId
                                                                                   && x.IsPublish == true);

            var list = new List<StudentSemesterViewModel>();
            foreach (var student in studentExam)
            {
                var sem = semesterExam.FirstOrDefault(x => x.Id == student.SemesterExamId);
                var studentSem = new StudentSemesterViewModel()
                {
                    Semester = sem.Semester,
                    SemesterYear = sem.SemesterYear,
                    ExamPublishedDate = sem.PublishDate,
                    IsPaid = student.IsPaid,
                    IsResult = isResult,
                };
                list.Add(studentSem);
            }

            var exams = list.DistinctBy(x => x.SemesterYear + "-" + x.Semester)
                             .OrderByDescending(x => x.SemesterYear)
                             .ThenByDescending(x => x.Semester)
                             .ToList();

            return exams.AsEnumerable();
        }

        public async Task<StudentExamViewModel> GetStudentExam(int sem, int year, bool isResult = false)
        {
            var universityId = await _studentService.GetUniversityId();
            var studentId = await _studentService.GetStudentId();
            var semesterExams = await GetStudentSemesterExam(studentId, universityId, sem, year, isResult);
            return semesterExams;
        }

        public async Task<StudentExamViewModel> GetStudentSemesterExam(Guid studentId, Guid universityId, int sem, int year, bool isResult = false)
        {

            var studentExam = await UnitOfWork.StudentExamRepository.FindAll(x => x.UniversityId == universityId
                                                                                 && x.StudentId == studentId);

            if (studentExam == null || studentExam.Count() == 0)
            {
                return new StudentExamViewModel();
            }

            var semesterExam = await UnitOfWork.SemesterExamRepository.FindAll(x => x.UniversityId == universityId
                                                                                   && x.IsPublish == true
                                                                                   && x.Semester == sem
                                                                                   && x.SemesterYear == year);

            var papers = await UnitOfWork.PaperRepository.GetAll();
            var semesterExams = new StudentExamViewModel();
            var currentPapers = new List<StudentExamModel>();
            var arrearPapers = new List<StudentExamModel>();
            decimal totalAmount = 0;

            foreach (var exam in studentExam)
            {
                var paper = papers.FirstOrDefault(x => x.Id == exam.PaperId);
                var semester = semesterExam.FirstOrDefault(x => x.Id == exam.SemesterExamId);
                if (semester != null)
                {
                    var examModel = new StudentExamModel()
                    {
                        StudentExamId = exam.Id,
                        Subject = paper.Name,
                        Marks = exam.Marks,
                        IsPass = exam.IsPass,
                        Attempt = exam.Attempt,
                        ExamDate = semester.ExamDate,
                        BaseSemester = paper.Semester,
                        IsResult = isResult,

                    };
                    if (semester.IsArrear)
                    {
                        arrearPapers.Add(examModel);
                    }
                    else
                    {
                        currentPapers.Add(examModel);
                    }
                    totalAmount += paper.Amount;
                    semesterExams.IsPaid = exam.IsPaid;
                    semesterExams.IsResult = exam.IsResult;
                    semesterExams.ResultDate = exam.ResultDate;
                    semesterExams.IsPublish = semester.IsPublish;
                    semesterExams.PublishDate = semester.PublishDate;
                }
            }
            semesterExams.StudentId = studentId;
            semesterExams.StudentName = await _studentService.GetStudentName(studentId);
            semesterExams.CurrentPapers = currentPapers;
            semesterExams.ArrearPapers = arrearPapers;
            semesterExams.Amount = totalAmount;
            semesterExams.Semester = sem;
            semesterExams.SemesterYear = year;

            return semesterExams;
        }

        public async Task UpdatePaidExamFee(int sem, int year)
        {
            if (sem == 0 || year == 0)
            {
                return;
            }

            var studentId = await _studentService.GetStudentId();
            var universityId = await _studentService.GetUniversityId();

            var studentExam = await UnitOfWork.StudentExamRepository.FindAll(x => x.UniversityId == universityId
                                                                                && x.StudentId == studentId
                                                                                && x.IsPaid == false);

            if (studentExam == null || studentExam.Count() == 0)
            {
                return;
            }

            var semesterExam = await UnitOfWork.SemesterExamRepository.FindAll(x => x.UniversityId == universityId
                                                                                   && x.IsPublish == true
                                                                                   && x.Semester == sem
                                                                                   && x.SemesterYear == year);


            foreach (var exam in studentExam)
            {
                var semester = semesterExam.FirstOrDefault(x => x.Id == exam.SemesterExamId);
                if (semester != null)
                {
                    exam.IsPaid = true;
                    exam.ModifiedBy = CurrentUserService.UserId;
                    exam.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;
                    UnitOfWork.StudentExamRepository.Update(exam);
                }
            }
            await UnitOfWork.Save();

        }


        #region degree certificate code
        public async Task<IActionResult> DegreeCertificateExportToPDF()
        {
            var studresponse = await _studentService.GetStudentProfile();
            var univresponse = await _universityService.Get(studresponse.UniversityId);
            CertificateViewModel certificateViewModel = new CertificateViewModel();
            certificateViewModel = new CertificateViewModel();
            certificateViewModel.Name = studresponse.Name;
            certificateViewModel.ClassType = "FIRST CLASS with DISTINCTION";
            certificateViewModel.DegreeName = studresponse.Program;
            certificateViewModel.Department = studresponse.Department;
            certificateViewModel.UniversityName = univresponse.Name;             // for export "Rotativa" used (wkhtmltopdf.exe)
            if (certificateViewModel != null)
            {
                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                return GeneratePDFForDegreeCertificate(certificateViewModel);
            }
            return null;
        }

        public ViewAsPdf GeneratePDFForDegreeCertificate(CertificateViewModel certificateViewModel)
        {
            return new ViewAsPdf("DegreeCertificateExportToPDF", certificateViewModel)
            {
                PageMargins = { Left = 0, Bottom = 5, Right = 10, Top = 5 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
            };
        }
        #endregion

        #region provisional certificate code
        public async Task<IActionResult> ProvisionalCertificateExportToPDF()
        {
            var studresponse = await _studentService.GetStudentProfile();
            var univresponse = await _universityService.Get(studresponse.UniversityId);
            CertificateViewModel certificateViewModel = new CertificateViewModel();
            certificateViewModel = new CertificateViewModel();
            certificateViewModel.Name = studresponse.Name;
            certificateViewModel.UniversityName = univresponse.Name;             // for export "Rotativa" used (wkhtmltopdf.exe)
            if (certificateViewModel != null)
            {
                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                return GeneratePDFForProvisionalCertificate(certificateViewModel);
            }
            return null;
        }

        public ViewAsPdf GeneratePDFForProvisionalCertificate(CertificateViewModel certificateViewModel)
        {
            return new ViewAsPdf("ProvisionalCertificateExportToPDF", certificateViewModel)
            {
                PageMargins = { Left = 0, Bottom = 5, Right = 10, Top = 5 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
            };
        }
        #endregion

        #region hall ticket code
        public async Task<IActionResult> HallTicketExportToPDF(int sem, int year)
        {
            var studresponse = await _studentService.GetStudentProfile();
            var univresponse = await _universityService.Get(studresponse.UniversityId);

            DateTime semesterMonth = new DateTime(2023, sem, 1);
            string semMonth_name = semesterMonth.ToString("MMM");

            CertificateViewModel certificateViewModel = new CertificateViewModel();
            certificateViewModel = new CertificateViewModel();
            certificateViewModel.Name = studresponse.Name;
            certificateViewModel.DOB = "01/01/2003";
            certificateViewModel.UniversityName = univresponse.Name;
            certificateViewModel.RollNo = studresponse.RollNumber;
            certificateViewModel.Gender = studresponse.Gender;
            certificateViewModel.Month = semMonth_name;
            certificateViewModel.Year = year.ToString();

            // for export "Rotativa" used (wkhtmltopdf.exe)
            if (certificateViewModel != null)
            {
                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                return HallTicketExportToPDF(certificateViewModel);
            }
            return null;
        }

        public ViewAsPdf HallTicketExportToPDF(CertificateViewModel certificateViewModel)
        {
            return new ViewAsPdf("HallTicketExportToPDF", certificateViewModel)
            {
                PageMargins = { Left = 0, Bottom = 5, Right = 10, Top = 5 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
            };
        }
        #endregion
    }
}
