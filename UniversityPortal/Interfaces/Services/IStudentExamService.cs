using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface IStudentExamService
    {
        Task<IEnumerable<StudentSemesterViewModel>> GetAllStudentSemester(bool isResult = false);
        Task<StudentExamViewModel> GetStudentExam(int sem, int year);
    }
}
