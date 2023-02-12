using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface IStudentExamService
    {
        Task<IEnumerable<StudentSemesterViewModel>> GetAllStudentSemester();
        Task<StudentExamViewModel> GetStudentExam(int sem, int year);
    }
}
