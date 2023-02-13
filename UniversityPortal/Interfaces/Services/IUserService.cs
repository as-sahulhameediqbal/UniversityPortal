using UniversityPortal.Common;

namespace UniversityPortal.Interfaces.Services
{
    public interface IUserService
    {
        Task<Guid> GetUserId(string email);
        Task<AppResponse> Create(string email, string password, string role);        
    }
}
