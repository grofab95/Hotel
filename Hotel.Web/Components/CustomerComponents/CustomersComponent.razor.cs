using Hotel.Application.Dtos;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.CustomerComponents
{
    public partial class CustomersComponent
    {
        [Inject] ICustomerDao CustomerDao { get; set; }

        private List<CustomerDto> _customers;
        private RadzenGrid<CustomerDto> _grid;
        private bool _firstLoad = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var customers = await CustomerDao.GetManyAsync(x => x.Id > 0);
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
                await CustomerDao.UpdateAsync(Mapper.Map<Customer>(customer));
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
                
                var customerDb = (await CustomerDao.GetManyAsync(x => x.Id == customer.Id)).FirstOrDefault();
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

                await CustomerDao.DeleteAsync(customer.Id);

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
