using AutoMapper;
using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;
using UniversityPortal.Services.Base;

namespace UniversityPortal.Services
{
    public class StudentExamService : BaseService, IStudentExamService
    {
        private readonly IUniversityStaffService _universityStaffService;
        private readonly IStudentService _studentService;
        public StudentExamService(IUnitOfWork unitOfWork,
                                  IMapper mapper,
                                  IDateTimeProvider dateTimeProvider,
                                  ICurrentUserService currentUserService,
                                  IUniversityStaffService universityStaffService,
                                  IStudentService studentService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {
            _universityStaffService = universityStaffService;
            _studentService = studentService;
        }


        public async Task<IEnumerable<StudentSemesterViewModel>> GetAllStudentSemester()
        {
            var universityId = await _studentService.GetUniversityId();
            var studentId = await _studentService.GetStudentId();
            var studentExam = await UnitOfWork.StudentExamRepository.FindAll(x => x.UniversityId == universityId
                                                                                 && x.StudentId == studentId);

            if (studentExam == null || studentExam.Count() == 0)
            {
                return Enumerable.Empty<StudentSemesterViewModel>();
            }

            var semesterExam = await UnitOfWork.SemesterExamRepository.FindAll(x => x.UniversityId == universityId
                                                                                    && x.IsPublish == true);

            var exams = semesterExam
                            .Select(x => new StudentSemesterViewModel
                            {
                                SemesterYear = x.SemesterYear,
                                Semester = x.Semester,
                                ExamPublishedDate = x.PublishDate
                            })
                            .DistinctBy(x => x.SemesterYear + "-" + x.Semester)
                            .OrderByDescending(x => x.SemesterYear)
                            .ThenByDescending(x => x.Semester)
                            .ToList();

            return exams.AsEnumerable();
        }

        public async Task<StudentExamViewModel> GetStudentExam(int sem, int year)
        {
            var universityId = await _studentService.GetUniversityId();
            var studentId = await _studentService.GetStudentId();
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
                        Subject = paper.Name,
                        Marks = exam.Marks,
                        IsPass = exam.IsPass,
                        Attempt = exam.Attempt,
                        ExamDate = semester.ExamDate,
                        BaseSemester = paper.Semester
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
            semesterExams.StudentName = await _studentService.GetStudentName();
            semesterExams.CurrentPapers = currentPapers;
            semesterExams.ArrearPapers = arrearPapers;
            semesterExams.Amount = totalAmount;
            semesterExams.Semester = sem;
            semesterExams.SemesterYear = year;

            return semesterExams;

        }
    }
}
