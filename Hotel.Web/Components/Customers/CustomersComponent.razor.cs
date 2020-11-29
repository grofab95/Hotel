using AutoMapper;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.Customers
{
    public partial class CustomersComponent
    {
        [Inject] ICustomerDao CustomerDao { get; set; }
        [Inject] IMapper Mapper { get; set; }

        private List<CustomerDto> _customers;
        private RadzenGrid<CustomerDto> _grid;
        private CustomerDto _customer;
        private bool _firstLoad = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var customers = await CustomerDao.GetAsync(x => x.Id > 0);
                _customers = Mapper.Map<List<CustomerDto>>(customers);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task SaveCustomer(CustomerDto customer)
        {
            try
            {
                await CustomerDao.UpdateCustomerAsync(Mapper.Map<Customer>(customer));
                _grid.CancelEditRow(customer);

                await ShowNotification("Zmiany zostały zapisane", Radzen.NotificationSeverity.Success);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private void EditCustomer(CustomerDto customer) => _grid.EditRow(customer);

        private async Task CancelEditCustomer(CustomerDto customer)
        {
            try
            {
                
                var customerDb = (await CustomerDao.GetAsync(x => x.Id == customer.Id)).FirstOrDefault();
                customer.FirstName = customerDb.FirstName;
                customer.LastName = customerDb.LastName;

                _grid.CancelEditRow(customer);

                StateHasChanged();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task DeleteCustomer(CustomerDto customer)
        {
            try
            {
                var isConfirm = await ShowConfirm($"Czy napewno chcesz usunąć klienta {customer}?");
                if (!isConfirm)
                    return;

                await CustomerDao.DeleteCustomerAsync(Mapper.Map<Customer>(customer));

                _customers.Remove(customer);

                await _grid.Reload();

                await ShowNotification("Klient został usunięty.", Radzen.NotificationSeverity.Success);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task OnCustomerCreated(Customer customer)
        {
            _customers.Add(Mapper.Map<CustomerDto>(customer));
            await _grid.Reload();

            window.Close();
            StateHasChanged();
        }
    }
}
