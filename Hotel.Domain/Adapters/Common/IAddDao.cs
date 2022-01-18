using System.Threading.Tasks;

namespace Hotel.Domain.Adapters.Common;

public interface IAddDao<T>
{
    Task<T> AddAsync(T entity);
}