using Microsoft.AspNetCore.Authentication;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Services;

namespace UniversityPortal.Common
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddTransient<IAccountService, AccountService>();

            return service;
        }
    }
}
