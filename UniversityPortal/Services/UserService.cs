using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UniversityPortal.Common;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Services.Base;

namespace UniversityPortal.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(IUnitOfWork unitOfWork,
                           IMapper mapper,
                           IDateTimeProvider dateTimeProvider,
                           ICurrentUserService currentUserService,
                           UserManager<IdentityUser> userManager,
                           SignInManager<IdentityUser> signInManager,
                           RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Guid> GetUserId(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Guid.Empty;
            }
            return new Guid(user.Id);
        }

        public async Task<AppResponse> Create(string email, string password, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return AppResult.msg(false, "Email address already exist");
            }

            var newuser = new IdentityUser()
            {
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newuser, password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                await _userManager.AddToRoleAsync(newuser, role);
            }

            return AppResult.msg(true, "Saved Successfully!");
        }
    }
}
