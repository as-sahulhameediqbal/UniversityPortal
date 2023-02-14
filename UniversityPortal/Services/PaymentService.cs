using AutoMapper;
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
        private readonly IStudentExamService _studentExamService;

        public PaymentService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              IDateTimeProvider dateTimeProvider,
                              ICurrentUserService currentUserService,
                              IStudentService studentService,
                              IStudentExamService studentExamService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {
            _studentService = studentService;
            _studentExamService = studentExamService;
        }

        public async Task<IEnumerable<PaymentViewModel>> GetAll()
        {
            var result = await UnitOfWork.PaymentRepository.GetAll();
            var university = Mapper.Map<IEnumerable<PaymentViewModel>>(result);
            return university;
        }

        public async Task Save(PaymentViewModel model)
        {
            if (model.FeesType == "TutionFees")
            {
                await _studentService.UpdatePaidTutionFee();
            }
            else
            {
                await _studentExamService.UpdatePaidExamFee(model.Sem, model.Year);
            }
        }
    }
}
