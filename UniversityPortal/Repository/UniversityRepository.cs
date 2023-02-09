using UniversityPortal.Data;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Repository.Base;

namespace UniversityPortal.Repository
{
    public class UniversityRepository : GenericRepository<University>, IUniversityRepository
    {
        public UniversityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
