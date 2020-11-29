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

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            if (await context.Customers.AnyAsync(x => x.FirstName.ToLower().Trim() == customer.FirstName.ToLower().Trim() &&
                                                      x.LastName.ToLower().Trim() == customer.LastName.ToLower().Trim()))
                throw new HotelException($"Klient {customer} już istnieje.");

            await context.AddAsync(customer);
            await context.SaveChangesAsync();

            return customer;
        }

        private async Task<bool> CheckIfExist(Customer customer)
        {
            return await context.Customers.AnyAsync(x => x.Id == customer.Id);
        }


        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            if (!(await CheckIfExist(customer)))
                throw new HotelException($"Klient {customer} nie istnieje.");

            AttachEntry(customer);

            await context.SaveChangesAsync();

            return customer;
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            if (!(await CheckIfExist(customer)))
                throw new HotelException($"Klient {customer} nie istnieje.");

            if (await context.Reservations.AnyAsync(x => x.Customer.Id == customer.Id))
                throw new HotelException($"Klient ma przypisane rezerwacje.");

            AttachEntry(customer);
            context.Remove(customer);

            await context.SaveChangesAsync();
        }
    }
}
