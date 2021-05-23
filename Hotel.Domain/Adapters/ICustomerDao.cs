using Hotel.Domain.Adapters.Common;
using Hotel.Domain.Entities;

namespace Hotel.Domain.Adapters
{
    public interface ICustomerDao : IAddDao<Customer>, IModifyDao<Customer>, IGetDao<Customer>
    {
    }
}
