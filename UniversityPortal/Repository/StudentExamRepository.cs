using UniversityPortal.Data;
using UniversityPortal.Entity;
using UniversityPortal.Interfaces.Repository;
using UniversityPortal.Repository.Base;

namespace UniversityPortal.Repository
{
    public class StudentExamRepository : GenericRepository<StudentExam>, IStudentExamRepository
    {
        public StudentExamRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
