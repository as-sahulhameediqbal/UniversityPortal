using AutoMapper;
using UniversityPortal.Common;
using UniversityPortal.Data;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;
using UniversityPortal.Services.Base;

namespace UniversityPortal.Services
{
    public class UniversityStaffService : BaseService, IUniversityStaffService
    {
        private readonly IUserService _userService;
        public UniversityStaffService(IUnitOfWork unitOfWork,
                                      IMapper mapper,
                                      IDateTimeProvider dateTimeProvider,
                                      ICurrentUserService currentUserService,
                                      IUserService userService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {
            _userService = userService;
        }

        public async Task<UniversityStaffViewModel> Get(Guid id)
        {
            var result = await UnitOfWork.UniversityStaffRepository.Get(id);
            var universityStaff = Mapper.Map<UniversityStaffViewModel>(result);
            universityStaff.Password = "Test";
            return universityStaff;

        }

        public async Task<IEnumerable<UniversityStaffViewModel>> GetAll()
        {
            var universityId = await GetUniversityId();
            var result = await UnitOfWork.UniversityStaffRepository.FindAll(x => x.UniversityId == universityId
                                                                               && x.Role != UserRoles.Admin);
            var university = Mapper.Map<IEnumerable<UniversityStaffViewModel>>(result);
            return university;
        }

        private async Task<Guid> GetUniversityId()
        {
            var result = await UnitOfWork.UniversityStaffRepository.FindAsync(x => x.IsActive
                                                                             && x.Email == CurrentUserService.Email);

            return result.UniversityId;
        }

        public async Task<AppResponse> Save(UniversityStaffViewModel model)
        {
            var result = await IsUniversityStaffExists(model);
            if (!result.Success)
            {
                return result;
            }

            if (model.Id == Guid.Empty)
            {
                model.UniversityId = await GetUniversityId();
                result = await _userService.Create(model.Email, model.Password, model.Role);
                if (!result.Success)
                {
                    return result;
                }
                await Add(model);
            }
            else
            {
                await Update(model);
            }

            await UnitOfWork.Save();

            return result;
        }

        public async Task<AppResponse> Create(UniversityStaffViewModel model, bool isAdd)
        {
            model.Id = Guid.Empty;
            if (!isAdd)
            {
                var universityStaff = await UnitOfWork.UniversityStaffRepository.FindAsync(x => x.IsActive
                                                                                   && x.Email == model.Email);
                model.Id = universityStaff.Id;
            }

            var result = await IsUniversityStaffExists(model);
            if (!result.Success)
            {
                return result;
            }
            if (isAdd)
            {
                result = await _userService.Create(model.Email, model.Password, model.Role);
                if (!result.Success)
                {
                    return result;
                }
                await Add(model);
            }
            else
            {
                await Update(model);
            }
            await UnitOfWork.Save();

            return AppResult.msg(true, "Saved SucessFully");
        }

        private async Task Add(UniversityStaffViewModel model)
        {
            var universityStaff = Mapper.Map<UniversityStaff>(model);

            universityStaff.Id = Guid.NewGuid();
            universityStaff.UserId = await _userService.GetUserId(model.Email);
            universityStaff.CreatedBy = CurrentUserService.UserId;
            universityStaff.CreatedDate = DateTimeProvider.DateTimeOffsetNow;
            universityStaff.ModifiedBy = CurrentUserService.UserId;
            universityStaff.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            await UnitOfWork.UniversityStaffRepository.Add(universityStaff);
        }

        private async Task Update(UniversityStaffViewModel model)
        {
            var universityStaff = await UnitOfWork.UniversityStaffRepository.Get(model.Id);

            universityStaff = Mapper.Map(model, universityStaff);

            universityStaff.ModifiedBy = CurrentUserService.UserId;
            universityStaff.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            UnitOfWork.UniversityStaffRepository.Update(universityStaff);
        }

        private async Task<AppResponse> IsUniversityStaffExists(UniversityStaffViewModel model)
        {
            if (model.Id == Guid.Empty)
            {
                var result = await UnitOfWork.UniversityStaffRepository.FindAny(x => x.IsActive
                                                                                    && x.Name == model.Name
                                                                                    && x.Email == model.Email);
                if (result)
                {
                    return AppResult.msg(false, "University Staff already exist");
                }
            }
            else
            {
                var result = await UnitOfWork.UniversityStaffRepository.FindAny(x => x.IsActive
                                                                               && x.Name == model.Name
                                                                               && x.Email == model.Email
                                                                               && x.Id != model.Id);
                if (result)
                {
                    return AppResult.msg(false, "University Staff already exist");
                }
            }

            return AppResult.msg(true, "University Staff not exist");
        }
    }
}
