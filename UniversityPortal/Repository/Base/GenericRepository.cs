using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using UniversityPortal.Data;
using UniversityPortal.Interfaces.Repository;

namespace UniversityPortal.Repository.Base
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        protected GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Get(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression);
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<bool> FindAny(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(expression) != null;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
           await _dbContext.Set<T>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }
        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

    }
}
