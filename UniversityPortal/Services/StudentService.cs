using AutoMapper;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Services.Base;
using UniversityPortal.Models;
using UniversityPortal.Data;
using UniversityPortal.Common;

namespace UniversityPortal.Services
{
    public class StudentService : BaseService, IStudentService
    {
        private readonly IUserService _userService;
        private readonly IUniversityStaffService _universityStaffService;
        public StudentService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              IDateTimeProvider dateTimeProvider,
                              ICurrentUserService currentUserService,
                              IUserService userService,
                              IUniversityStaffService universityStaffService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {
            _userService = userService;
            _universityStaffService = universityStaffService;
        }


        public async Task<StudentViewModel> Get(Guid id)
        {
            var result = await UnitOfWork.StudentRepository.Get(id);
            var student = Mapper.Map<StudentViewModel>(result);
            student.Password = nameof(student.Password);
            return student;

        }
        public async Task<IEnumerable<StudentViewModel>> GetAll()
        {
            var universityId = await _universityStaffService.GetUniversityId();
            var result = await UnitOfWork.StudentRepository.FindAll(x => x.UniversityId == universityId);
            var student = Mapper.Map<IEnumerable<StudentViewModel>>(result);
            return student;
        }

        public async Task<AppResponse> Save(StudentViewModel model)
        {
            var result = await IsStudentExists(model);
            if (!result.Success)
            {
                return result;
            }

            if (model.Id == Guid.Empty)
            {
                model.UniversityId = await _universityStaffService.GetUniversityId();
                result = await _userService.Create(model.Email, model.Password, UserRoles.Student);
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

        private async Task Add(StudentViewModel model)
        {
            var student = Mapper.Map<Student>(model);

            student.Id = Guid.NewGuid();
            student.UserId = await _userService.GetUserId(model.Email);
            student.CreatedBy = CurrentUserService.UserId;
            student.CreatedDate = DateTimeProvider.DateTimeOffsetNow;
            student.ModifiedBy = CurrentUserService.UserId;
            student.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            await UnitOfWork.StudentRepository.Add(student);
        }

        private async Task Update(StudentViewModel model)
        {
            var student = await UnitOfWork.StudentRepository.Get(model.Id);

            student = Mapper.Map(model, student);

            student.ModifiedBy = CurrentUserService.UserId;
            student.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            UnitOfWork.StudentRepository.Update(student);
        }

        private async Task<AppResponse> IsStudentExists(StudentViewModel model)
        {
            if (model.Id == Guid.Empty)
            {
                var result = await UnitOfWork.StudentRepository.FindAny(x => x.IsActive
                                                                          && x.Name == model.Name
                                                                          && x.Email == model.Email);
                if (result)
                {
                    return AppResult.msg(false, "Student already exist");
                }
            }
            else
            {
                var result = await UnitOfWork.StudentRepository.FindAny(x => x.IsActive
                                                                             && x.Name == model.Name
                                                                            && x.Email == model.Email
                                                                            && x.Id != model.Id);
                if (result)
                {
                    return AppResult.msg(false, "Student already exist");
                }
            }

            return AppResult.msg(true, "Student not exist");
        }

    }
}
