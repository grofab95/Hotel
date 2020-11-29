using Hotel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface ICustomerDao
    {
        Task<List<Customer>> GetAsync(Expression<Func<Customer, bool>> expression);
        Task<Customer> AddCustomer(Customer customer);
    }
}
