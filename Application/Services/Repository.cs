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
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly DbContext _context;
        private DbSet<T> entities;

        public Repository(DbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            await entities.AddAsync(entity);
            return await SaveDbChangesAsync();
        }

        public async Task<T> AddWithReturnAsync(T entity)
        {
            await entities.AddAsync(entity);
            await SaveDbChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int entityId)
        {
            T currentEntity = await entities.SingleOrDefaultAsync(s => s.Id == entityId);
            currentEntity.IsActive = false;
            return await SaveDbChangesAsync();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return entities.Where(predicate);
        }

        public async Task<bool> SaveDbChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await entities.SingleOrDefaultAsync(predicate);
        }


        async Task<IEnumerable<T>> IRepository<T>.GetAllAsync()
        {
            return await entities.Where(s=> s.IsActive == true).AsNoTracking().ToListAsync();
        }

        async ValueTask<T> IRepository<T>.GetByIdAsync(int id)
        {
            return await entities.Where(s=> s.IsActive == true && s.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
