using UniversityPortal.Data;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Repository.Base;

namespace UniversityPortal.Repository
{
    public class PaperRepository : GenericRepository<Paper>, IPaperRepository
    {
        public PaperRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
