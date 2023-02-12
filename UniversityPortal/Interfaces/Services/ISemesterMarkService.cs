using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface ISemesterMarkService
    {
        Task<IEnumerable<SemesterViewModel>> GetSemesterAll();
        Task<SemesterStudentViewModel> GetSemesterStudent(int sem, int year);
    }
}
