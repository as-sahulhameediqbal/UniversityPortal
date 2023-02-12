using AutoMapper;
using UniversityPortal.Entity;
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
        public SemesterMarkService(IUnitOfWork unitOfWork,
                                   IMapper mapper,
                                   IDateTimeProvider dateTimeProvider,
                                   ICurrentUserService currentUserService,
                                   IUniversityStaffService universityStaffService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {
            _universityStaffService = universityStaffService;
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
                var studentExam = studentExams.FirstOrDefault(x => x.SemesterExamId == exam.Id);
                var sem = new SemesterViewModel()
                {
                    Semester = exam.Semester,
                    SemesterYear = exam.SemesterYear,
                    IsResult = studentExam.IsResult,
                    ResultDate = studentExam.ResultDate
                };
                list.Add(sem);
            }

            var exams = list.DistinctBy(x => x.SemesterYear + "-" + x.Semester)
                             .OrderByDescending(x => x.SemesterYear)
                             .ThenByDescending(x => x.Semester)
                             .ToList();

            return exams.AsEnumerable();
        }


        public async Task<SemesterStudentViewModel> GetSemesterStudent(int sem, int year)
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

            foreach (var exam in studentExams)
            {
                var semester = semesterExam.FirstOrDefault(x => x.Id == exam.SemesterExamId);

                semesterStudent.IsPublish = semester.IsPublish;
                semesterStudent.PublishDate = semester.PublishDate;
                semesterStudent.IsResult = exam.IsResult;
                semesterStudent.ResultDate = exam.ResultDate;
            }

            return semesterStudent;
        }
    }
}
