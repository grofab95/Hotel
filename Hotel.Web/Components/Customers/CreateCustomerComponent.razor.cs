using AutoMapper;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Hotel.Web.Components.Customers
{
    public partial class CreateCustomerComponent
    {
        [Parameter] public EventCallback<Customer> OnCreated { get; set; }
        [Inject] ICustomerDao CustomerDao { get; set; }
        [Inject] IMapper Mapper { get; set; }

        private CustomerDto _newCustomer = new CustomerDto();

        private async Task Create(CustomerDto newCustomer)
        {
            try
            {
                var customer = await CustomerDao.AddCustomerAsync(Mapper.Map<Customer>(newCustomer));

                await ShowNotification("Dodano pomyślnie", Radzen.NotificationSeverity.Success);
                await OnCreated.InvokeAsync(customer);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }
}
