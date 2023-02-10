using System.Linq.Expressions;

namespace UniversityPortal.Interfaces.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task<T> FindAsync(Expression<Func<T, bool>> expression);
        Task<bool> FindAny(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
