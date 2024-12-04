using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using LogTransformer.Core.Entities;
using System.Threading.Tasks;

namespace LogTransformer.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync();
    }
}