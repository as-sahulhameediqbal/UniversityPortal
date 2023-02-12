using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface IStudentExamService
    {
        Task<IEnumerable<StudentSemesterViewModel>> GetAllStudentSemester();
    }
}
