using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;

namespace Application.Repositories
{
    public interface IRepository<T> where T: BaseModel
    {
        ValueTask<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AddAsync(T entity);
        Task<bool> SaveDbChangesAsync();
        Task<bool> DeleteAsync(int entityId);
    }
}
