using AutoMapper;
using UniversityPortal.Common;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Models;
using UniversityPortal.Services.Base;

namespace UniversityPortal.Services
{
    public class PaymentService : BaseService, IPaymentService
    {

        private readonly IStudentService _studentService;

        public PaymentService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              IDateTimeProvider dateTimeProvider,
                              ICurrentUserService currentUserService,
                              IStudentService studentService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {
            _studentService = studentService;
        }

        public async Task<IEnumerable<PaymentViewModel>> GetAll()
        {
            var result = await UnitOfWork.PaymentRepository.GetAll();
            var university = Mapper.Map<IEnumerable<PaymentViewModel>>(result);
            return university;
        }
                
        public async Task<int> Save(PaymentViewModel model)
        {
            var id = await _studentService.GetStudentId();
            var response = await _studentService.Get(id);
            await Update(response);

            var result = await UnitOfWork.Save();

            return result;
        }

        private async Task Update(StudentViewModel model)
        {
            var payment = await UnitOfWork.StudentRepository.Get(model.Id);

            payment = Mapper.Map(model, payment);
            payment.IsPaid = true;
            payment.ModifiedBy = CurrentUserService.UserId;
            payment.ModifiedDate = DateTimeProvider.DateTimeOffsetNow;

            UnitOfWork.StudentRepository.Update(payment);
        }
    }
}
