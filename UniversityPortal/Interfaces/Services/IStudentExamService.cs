using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface IStudentExamService
    {
        Task<IEnumerable<StudentSemesterViewModel>> GetAllStudentSemester(bool isResult = false);
        Task<StudentExamViewModel> GetStudentExam(int sem, int year);
        Task<StudentExamViewModel> GetStudentSemesterExam(Guid studentId, Guid universityId, int sem, int year);
    }
}
