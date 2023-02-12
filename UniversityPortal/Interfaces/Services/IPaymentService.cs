using UniversityPortal.Common;
using UniversityPortal.Models;

namespace UniversityPortal.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentViewModel>> GetAll();
        Task<int> Save(PaymentViewModel model);        
    }
}
