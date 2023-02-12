using UniversityPortal.Data;
using UniversityPortal.Interfaces.Repository;

namespace UniversityPortal.Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            UniversityRepository = new UniversityRepository(dbContext);
            UniversityStaffRepository = new UniversityStaffRepository(dbContext);
            StudentRepository = new StudentRepository(dbContext);
            PaperRepository = new PaperRepository(dbContext);
            StudentExamRepository = new StudentExamRepository(dbContext);
            SemesterExamRepository = new SemesterExamRepository(dbContext);

        }

        public IUniversityRepository UniversityRepository { get; private set; }
        public IUniversityStaffRepository UniversityStaffRepository { get; private set; }
        public IStudentRepository StudentRepository { get; private set; }
        public IPaperRepository PaperRepository { get; private set; }
        public IStudentExamRepository StudentExamRepository { get; private set; }
        public ISemesterExamRepository SemesterExamRepository { get; private set; }



        public async Task<int> Save()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
