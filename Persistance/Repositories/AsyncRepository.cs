using System;
using System.Linq;
using Application.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories
{
    public class AsyncRepository<T> : IAsyncRepository<T> where T : class
    {
        readonly BankDbContext dbContext;
        public AsyncRepository(BankDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {

            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<T> GetAsync(string id)
        {
            return await dbContext.Set<T>().FindAsync(id);

        }

        public async Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}
