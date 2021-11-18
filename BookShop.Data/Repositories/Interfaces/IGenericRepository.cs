using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IGenericRepository<T, in TArg> where T : class
    {
        Task<T> GetAsync(TArg id);
        Task<IReadOnlyList<T>> GetAsync(IEnumerable<TArg> ids);
        Task<IReadOnlyList<T>> GetAsync();
        Task<int> CreateAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(TArg id);
    }
}