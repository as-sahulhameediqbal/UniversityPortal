using AutoMapper;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Services.Base;
using UniversityPortal.Models;
namespace UniversityPortal.Services
{
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              IDateTimeProvider dateTimeProvider,
                              ICurrentUserService currentUserService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {

        }

        public async Task AddStudent(StudentRegistrationViewModel studentViewModel)
        {
            var student = Mapper.Map<Student>(studentViewModel);

            studentViewModel.Id = Guid.NewGuid();
            studentViewModel.CreatedBy = CurrentUserService.UserId;
            studentViewModel.CreatedDate = DateTimeProvider.DateTimeOffsetNow;
            studentViewModel.ModifiedBy = CurrentUserService.UserId;
            studentViewModel.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            await UnitOfWork.StudentRepository.Add(student);
        }
        public async Task AddFee(FeesViewModel model)
        {
            var fee = Mapper.Map<Fees>(model);

            model.Id = Guid.NewGuid();
            model.CreatedBy = CurrentUserService.UserId;
            model.CreatedDate = DateTimeProvider.DateTimeOffsetNow;
            model.ModifiedBy = CurrentUserService.UserId;
            model.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            await UnitOfWork.StudentRepository.Add(fee);
        }

        public async Task AddMarks(MarksViewModel model)
        {
            var marks = Mapper.Map<Marks>(model);

            model.Id = Guid.NewGuid();
            model.CreatedBy = CurrentUserService.UserId;
            model.CreatedDate = DateTimeProvider.DateTimeOffsetNow;
            model.ModifiedBy = CurrentUserService.UserId;
            model.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            await UnitOfWork.StudentRepository.Add(marks);
        }
    }
}
