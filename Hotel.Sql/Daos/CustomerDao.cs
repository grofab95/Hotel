using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class CustomerDao : BaseDao, ICustomerDao
    {
        public CustomerDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        { }

        public async Task<List<Customer>> GetAsync(Expression<Func<Customer, bool>> expression)
            => await context.Customers
                .Where(expression)
                .OrderBy(x => x.FirstName)
                .ToListAsync();

        public async Task<int> AddCustomer(Customer customer)
        {
            var isExist = await context.Customers
                .AnyAsync(x => x.FirstName.ToLower() == customer.FirstName.ToLower() &&
                          x.LastName.ToLower() == customer.LastName.ToLower());

            if (isExist)
                throw new HotelException($"Klient {customer} już istnieje.");

            AttachEntry(customer);
            await context.SaveChangesAsync();

            return customer.Id;
        }
    }
}
