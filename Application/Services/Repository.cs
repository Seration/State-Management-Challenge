using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class Repository<T> : IRepository<T> where T:BaseModel
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
           _context = context;
        }

        public async System.Threading.Tasks.Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public async Task<bool> SaveDbChangesAsync()
        {
          return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }


        async Task<IEnumerable<T>> IRepository<T>.GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        async ValueTask<T> IRepository<T>.GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
