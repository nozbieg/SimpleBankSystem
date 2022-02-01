using System;
using System.Linq;

namespace Application.Contracts;

public interface IAsyncRepository<T>
{
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> GetAsync(string id);
}
