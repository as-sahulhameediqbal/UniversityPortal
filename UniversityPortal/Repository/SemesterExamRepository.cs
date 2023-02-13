using UniversityPortal.Data;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Repository.Base;

namespace UniversityPortal.Repository
{
    public class SemesterExamRepository : GenericRepository<SemesterExam>, ISemesterExamRepository
    {
        public SemesterExamRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
