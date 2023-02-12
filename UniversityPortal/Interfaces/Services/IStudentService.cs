using UniversityPortal.Common;
using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface IStudentService
    {
        Task<StudentViewModel> Get(Guid id);
        Task<IEnumerable<StudentViewModel>> GetAll();
        Task<AppResponse> Save(StudentViewModel model);
        Task<Guid> GetUniversityId();
        Task<Guid> GetStudentId();
    }
}
