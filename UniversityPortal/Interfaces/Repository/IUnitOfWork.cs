namespace UniversityPortal.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        public IUniversityRepository UniversityRepository { get; }
        public IStudentRepository StudentRepository { get; }
    }
}
