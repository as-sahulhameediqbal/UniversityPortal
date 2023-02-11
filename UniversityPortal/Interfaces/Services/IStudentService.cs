using UniversityPortal.Common;
using UniversityPortal.Models;
namespace UniversityPortal.Interfaces.Services
{
    public interface IStudentService
    {
        Task<AppResponse> AddStudent(StudentRegistrationViewModel model);
        Task<AppResponse> AddFee(FeesViewModel model);
        Task<AppResponse> AddMarks(MarksViewModel marksViewModel);
    }
}
