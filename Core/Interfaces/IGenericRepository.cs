using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityBySpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsyncBySpec(ISpecification<T> spec);
    }
}
