using System;
using System.Linq;
using Application.Contracts;

namespace Persistance.Repositories
{
    public class AsyncRepository<T> : IAsyncRepository<T>
    {
        public Task<T> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
