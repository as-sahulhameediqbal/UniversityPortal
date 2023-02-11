using AutoMapper;
using UniversityPortal.Interfaces.Common;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Interfaces.Services;
using UniversityPortal.Services.Base;

namespace UniversityPortal.Services
{
    public class StudentExamService : BaseService, IStudentExamService
    {
        public StudentExamService(IUnitOfWork unitOfWork,
                                  IMapper mapper,
                                  IDateTimeProvider dateTimeProvider,
                                  ICurrentUserService currentUserService) : base(unitOfWork, mapper, dateTimeProvider, currentUserService)
        {

        }
    }
}
