using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentViewModel>> GetAll();
        Task Save(PaymentViewModel model);
    }
}
