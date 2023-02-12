using AutoMapper;
using UniversityPortal.Entity;
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

    }
}
