using AutoMapper;
using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;
using UniversityPortal.Services.Base;

namespace UniversityPortal.Services
{
    public class SemesterMarkService : BaseService, ISemesterMarkService
    {
        private readonly IUniversityStaffService _universityStaffService;
        private readonly IStudentExamService _studentExamService;
        public SemesterMarkService(IUnitOfWork unitOfWork,
                                   IMapper mapper,
                                   IDateTimeProvider dateTimeProvider,
                                   ICurrentUserService currentUserService,
                                   IUniversityStaffService universityStaffService,
                                   IStudentExamService studentExamService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {
            _universityStaffService = universityStaffService;
            _studentExamService = studentExamService;
        }

        public async Task<IEnumerable<SemesterViewModel>> GetSemesterAll()
        {
            var universityId = await _universityStaffService.GetUniversityId();
            var semesterExam = await UnitOfWork.SemesterExamRepository.FindAll(x => x.UniversityId == universityId
                                                                                   && x.IsPublish == true);

            if (semesterExam == null || semesterExam.Count() == 0)
            {
                return Enumerable.Empty<SemesterViewModel>();
            }

            var list = new List<SemesterViewModel>();
            var studentExams = await UnitOfWork.StudentExamRepository.FindAll(x => x.UniversityId == universityId);

            foreach (var exam in semesterExam)
            {
                var studentExam = studentExams.FirstOrDefault(x => x.SemesterExamId == exam.Id && x.IsPaid == true);
                if (studentExam != null)
                {
                    var sem = new SemesterViewModel()
                    {
                        Semester = exam.Semester,
                        SemesterYear = exam.SemesterYear,
                        IsResult = studentExam.IsResult,
                        ResultDate = studentExam.ResultDate
                    };
                    list.Add(sem);
                }
            }

            var exams = list.DistinctBy(x => x.SemesterYear + "-" + x.Semester)
                             .OrderByDescending(x => x.SemesterYear)
                             .ThenByDescending(x => x.Semester)
                             .ToList();

            return exams.AsEnumerable();
        }


        public async Task<SemesterStudentViewModel> GetSemesterStudentAll(int sem, int year)
        {
            var universityId = await _universityStaffService.GetUniversityId();
            var studentExams = await UnitOfWork.StudentExamRepository.FindAll(x => x.UniversityId == universityId);

            if (studentExams == null || studentExams.Count() == 0)
            {
                return new SemesterStudentViewModel();
            }

            var semesterExam = await UnitOfWork.SemesterExamRepository.FindAll(x => x.UniversityId == universityId
                                                                                    && x.IsPublish == true
                                                                                    && x.Semester == sem
                                                                                    && x.SemesterYear == year);

            var students = await UnitOfWork.StudentRepository.FindAll(x => x.UniversityId == universityId);

            var semesterStudent = new SemesterStudentViewModel();

            semesterStudent.Students = students
                                .Select(x => new StudentModel
                                {
                                    StudentId = x.Id,
                                    StudentName = x.Name,
                                    RollNumber = x.RollNumber,
                                    Program = x.Program,
                                    Department = x.Department,
                                    Semester = sem,
                                    SemesterYear = year
                                })
                                .ToList()
                                .AsEnumerable();


            semesterStudent.Semester = sem;
            semesterStudent.SemesterYear = year;
            semesterStudent.IsPublishResult = studentExams.Any(x => x.IsPass);

            foreach (var exam in studentExams)
            {
                var semester = semesterExam.FirstOrDefault(x => x.Id == exam.SemesterExamId);
                if (semester != null)
                {
                    semesterStudent.IsPublish = semester.IsPublish;
                    semesterStudent.PublishDate = semester.PublishDate;
                    semesterStudent.IsResult = exam.IsResult;
                    semesterStudent.ResultDate = exam.ResultDate;
                }
            }

            return semesterStudent;
        }

        public async Task<StudentModel> GetSemesterStudent(Guid studentId, int sem, int year)
        {
            var universityId = await _universityStaffService.GetUniversityId();
            var student = await UnitOfWork.StudentRepository.Get(studentId);
            if (student == null)
            {
                return new StudentModel();
            }

            var studentModel = new StudentModel()
            {
                StudentId = student.Id,
                StudentName = student.Name,
                RollNumber = student.RollNumber,
                Program = student.Program,
                Department = student.Department,
                Semester = sem,
                SemesterYear = year
            };

            var studentExams = await _studentExamService.GetStudentSemesterExam(studentId, universityId, sem, year);

            if (studentExams != null)
            {
                studentModel.CurrentPapers = studentExams.CurrentPapers;
                studentModel.ArrearPapers = studentExams.ArrearPapers;
            }

            return studentModel;
        }

        public async Task PublishMarkResult(int sem, int year)
        {
            var universityId = await _universityStaffService.GetUniversityId();
            var semesterExams = await UnitOfWork.SemesterExamRepository.FindAll(x => x.UniversityId == universityId
                                                                                    && x.IsPublish == true
                                                                                    && x.Semester == sem
                                                                                    && x.SemesterYear == year);


            if (semesterExams == null || semesterExams.Count() == 0)
            {
                return;
            }

            var studentExams = await UnitOfWork.StudentExamRepository.FindAll(x => x.UniversityId == universityId && x.IsResult == false);

            foreach (var exam in studentExams)
            {
                var semester = semesterExams.FirstOrDefault(x => x.Id == exam.SemesterExamId);
                if (semester != null)
                {
                    exam.IsResult = true;
                    exam.ResultDate = DateTimeProvider.DateTimeOffsetNow;
                    exam.ModifiedBy = CurrentUserService.UserId;
                    exam.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;
                    UnitOfWork.StudentExamRepository.Update(exam);
                }
            }
            await UnitOfWork.Save();
        }
    }
}
