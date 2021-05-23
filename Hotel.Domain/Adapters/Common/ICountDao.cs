using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters.Common
{
    public interface ICountDao<T>
    {
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
