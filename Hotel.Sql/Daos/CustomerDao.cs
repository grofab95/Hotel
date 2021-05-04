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

        public async Task<List<Customer>> GetManyAsync(Expression<Func<Customer, bool>> expression)
            => await context.Customers
                .Where(expression)
                .OrderBy(x => x.Name)
                .ToListAsync();

        public async Task<Customer> AddAsync(Customer customer)
        {
            if (await context.Customers.AnyAsync(x => x.Name.ToLower().Trim() == customer.Name.ToLower().Trim()))
                throw new HotelException($"Klient {customer} już istnieje.");

            await context.AddAsync(customer);
            await context.SaveChangesAsync();

            return customer;
        }

        private async Task<bool> CheckIfExist(int id)
        {
            return await context.Customers.AnyAsync(x => x.Id == id);
        }


        public async Task<Customer> UpdateAsync(Customer customer)
        {
            if (!(await CheckIfExist(customer.Id)))
                throw new HotelException($"Klient {customer} nie istnieje.");

            AttachEntry(customer);

            await context.SaveChangesAsync();

            return customer;
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new HotelException($"Klient o id {id} nie istnieje.");

            if (await context.Reservations.AnyAsync(x => x.Customer.Id == id))
                throw new HotelException($"Klient ma przypisane rezerwacje.");

            AttachEntry(customer);
            context.Remove(customer);

            await context.SaveChangesAsync();
        }

        public async Task<Customer> GetAsync(Expression<Func<Customer, bool>> predicate)
        {
            return await context.Customers.FirstOrDefaultAsync(predicate)
                ?? throw new HotelException($"Klient nie został odnaleziony");
        }
    }
}
