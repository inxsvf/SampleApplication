using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SampleApplication.Repository.Models;
using SampleApplication.Repository.Exceptions;

namespace SampleApplication.Repository
{
    public interface IBaseRepository<T> : IDisposable where T : BaseEntity
    {
        public Task<IList<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> InsertAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task DeleteByIdAsync(int id);
    }

    public class BaseRepository<T>: IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var item = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

            return item;
        }

        public async Task<T> InsertAsync(T entity)
        {
            var a = await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return a.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var exists = await _dbSet.AnyAsync(e => e.Id == entity.Id);
            if (!exists) throw new NotFoundException();

            var item = _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();

            return item.Entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            await _dbContext.SaveChangesAsync();

            return;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item == null) throw new NotFoundException();

            await DeleteAsync(item);

            return;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
