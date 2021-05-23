using Hotel.Application.Dtos.CustomerDtos;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Hotel.Web.Components.CustomerComponents
{
    public partial class CreateCustomerComponent
    {
        [Parameter] public EventCallback<Customer> OnCreated { get; set; }
        [Inject] ICustomerDao CustomerDao { get; set; }

        private CustomerGetDto _newCustomer = new CustomerGetDto();

        private async Task Create(CustomerGetDto newCustomer)
        {
            try
            {
                var customer = await CustomerDao.AddAsync(Mapper.Map<Customer>(newCustomer));

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
