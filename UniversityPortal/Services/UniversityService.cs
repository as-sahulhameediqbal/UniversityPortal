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
    public class UniversityService : BaseService, IUniversityService
    {
        private readonly IUniversityStaffService _universityStaffService;
        public UniversityService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IDateTimeProvider dateTimeProvider,
                                 ICurrentUserService currentUserService,
                                 IUniversityStaffService universityStaffService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {
            _universityStaffService = universityStaffService;
        }

       
        public async Task<UniversityViewModel> Get(Guid id)
        {
            var result = await UnitOfWork.UniversityRepository.Get(id);
            var university = Mapper.Map<UniversityViewModel>(result);
            university.Password = "Test";
            return university;
        }

        public async Task<IEnumerable<UniversityViewModel>> GetAll()
        {
            var result = await UnitOfWork.UniversityRepository.GetAll();
            var university = Mapper.Map<IEnumerable<UniversityViewModel>>(result);
            return university;
        }

        private async Task<Guid> GetId(string email)
        {
            var result = await UnitOfWork.UniversityRepository.FindAsync(x => x.IsActive
                                                                             && x.Email == email);

            return result.Id;
        }

        public async Task<AppResponse> Save(UniversityViewModel model)
        {
            var result = await IsUniversityExists(model);
            if (!result.Success)
            {
                return result;
            }
            if (model.Id == Guid.Empty)
            {
                await Add(model);
            }
            else
            {
                await Update(model);
            }

            await UnitOfWork.Save();

            var universityStaff = Mapper.Map<UniversityStaffViewModel>(model);
            if (model.Id == Guid.Empty)
            {
                universityStaff.UniversityId = await GetId(model.Email);
                universityStaff.Role = UserRoles.Admin;
            }
            result = await _universityStaffService.Create(universityStaff, (model.Id == Guid.Empty));

            return result;
        }

        private async Task Add(UniversityViewModel model)
        {
            var university = Mapper.Map<University>(model);

            university.Id = Guid.NewGuid();
            university.CreatedBy = CurrentUserService.UserId;
            university.CreatedDate = DateTimeProvider.DateTimeOffsetNow;
            university.ModifiedBy = CurrentUserService.UserId;
            university.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            await UnitOfWork.UniversityRepository.Add(university);
        }

        private async Task Update(UniversityViewModel model)
        {
            var university = await UnitOfWork.UniversityRepository.Get(model.Id);

            university = Mapper.Map(model, university);

            university.ModifiedBy = CurrentUserService.UserId;
            university.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            UnitOfWork.UniversityRepository.Update(university);
        }

        private async Task<AppResponse> IsUniversityExists(UniversityViewModel model)
        {
            if (model.Id == Guid.Empty)
            {
                var result = await UnitOfWork.UniversityRepository.FindAny(x => x.IsActive
                                                                             && x.Name == model.Name
                                                                             && x.Code == model.Code
                                                                             && x.Email == model.Email);
                if (result)
                {
                    return AppResult.msg(false, "University already exist");
                }
            }
            else
            {
                var result = await UnitOfWork.UniversityRepository.FindAny(x => x.IsActive
                                                                               && x.Name == model.Name
                                                                               && x.Code == model.Code
                                                                               && x.Email == model.Email
                                                                               && x.Id != model.Id);
                if (result)
                {
                    return AppResult.msg(false, "University already exist");
                }
            }

            return AppResult.msg(true, "University not exist");
        }

    }
}
