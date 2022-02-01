using System;
using System.Linq;

namespace Application.Contracts;

public interface IAsyncRepository<T>
{
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(string id);
    Task<T> DeleteAsync(string id);
    Task<T> GetAsync(string id);
}
