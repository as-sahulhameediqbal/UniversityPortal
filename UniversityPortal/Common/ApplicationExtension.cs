using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Repository.Base;
using UniversityPortal.Services;

namespace UniversityPortal.Common
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            service.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            service.AddSingleton<ICurrentUserService, CurrentUserService>();

            service.AddTransient<IAccountService, AccountService>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IUniversityService, UniversityService>();
            service.AddTransient<IUniversityStaffService, UniversityStaffService>();
            service.AddTransient<IStudentService, StudentService>();
            service.AddTransient<IPaperService, PaperService>();
            service.AddTransient<IStudentExamService, StudentExamService>();
            service.AddTransient<ISemesterExamService, SemesterExamService>();
            service.AddTransient<IPaymentService, PaymentService>();

            service.AddTransient<IUnitOfWork, UnitOfWork>();
            return service;
        }
    }
}
