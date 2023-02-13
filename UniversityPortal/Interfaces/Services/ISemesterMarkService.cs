using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface ISemesterMarkService
    {
        Task<IEnumerable<SemesterViewModel>> GetSemesterAll();
        Task<SemesterStudentViewModel> GetSemesterStudentAll(int sem, int year);
        Task<StudentModel> GetSemesterStudent(Guid studentId, int sem, int year);
        Task PublishMarkResult(int sem, int year);        
    }
}
