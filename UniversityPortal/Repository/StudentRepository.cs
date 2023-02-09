using UniversityPortal.Data;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Repository.Base;

namespace UniversityPortal.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
