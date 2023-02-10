using UniversityPortal.Data;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Repository.Base;

namespace UniversityPortal.Repository
{
    public class UniversityStaffRepository : GenericRepository<UniversityStaff>, IUniversityStaffRepository
    {
        public UniversityStaffRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
