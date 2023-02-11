using AutoMapper;
using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Services.Base;

namespace UniversityPortal.Services
{
    public class PaperService : BaseService, IPaperService
    {
        public PaperService(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IDateTimeProvider dateTimeProvider,
                            ICurrentUserService currentUserService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {

        }
    }
}
