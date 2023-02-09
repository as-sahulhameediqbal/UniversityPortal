using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface IAccountService
    {
        Task<bool> Login(LoginViewModel login);
    }
}
