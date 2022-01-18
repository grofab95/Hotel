using System.Threading.Tasks;

namespace Hotel.Domain.Adapters.Common;

public interface IModifyDao<T>
{
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(int id);
}