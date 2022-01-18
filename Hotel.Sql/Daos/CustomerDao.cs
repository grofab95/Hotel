using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos;

public class CustomerDao : BaseDao<Customer>, ICustomerDao
{
    public CustomerDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
    { }

    private async Task<bool> CheckIfExist(int id)
    {
        return await context.Customers.AnyAsync(x => x.Id == id);
    }


    public override async Task<Customer> UpdateAsync(Customer customer)
    {
        if (!(await CheckIfExist(customer.Id)))
            throw new HotelException($"Klient {customer} nie istnieje.");

        await base.UpdateAsync(customer);
        return customer;
    }

    public override async Task DeleteAsync(int id)
    {
        if (await context.Reservations.AnyAsync(x => x.Customer.Id == id))
            throw new HotelException($"Klient ma przypisane rezerwacje.");

        await base.DeleteAsync(id);
    }
}