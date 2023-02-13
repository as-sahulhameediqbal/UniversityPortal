using UniversityPortal.Common;
using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface IUniversityService
    {
        Task<UniversityViewModel> Get(Guid id);
        Task<string> GetName(Guid id);
        Task<IEnumerable<UniversityViewModel>> GetAll();
        Task<AppResponse> Save(UniversityViewModel model);
    }
}
