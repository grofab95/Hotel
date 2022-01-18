using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters.Common;

public interface IGetDao<T>
{
    Task<List<T>> GetManyAsync(int page, int limit, Expression<Func<T, bool>> predicate);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);
}