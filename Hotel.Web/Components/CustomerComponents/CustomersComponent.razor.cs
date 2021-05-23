using Hotel.Application.Dtos.CustomerDtos;
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

        private List<CustomerGetDto> _customers;
        private RadzenGrid<CustomerGetDto> _grid;
        private bool _firstLoad = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var customers = await CustomerDao.GetManyAsync(1, 1000, x => x.Id > 0);  // todo: implement paggination
                _customers = Mapper.Map<List<CustomerGetDto>>(customers);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task SaveCustomer(CustomerGetDto customer)
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

        private void EditCustomer(CustomerGetDto customer) => _grid.EditRow(customer);

        private async Task CancelEditCustomer(CustomerGetDto customer)
        {
            try
            {

                var customerDb = await CustomerDao.GetAsync(x => x.Id == customer.Id);
                customer.Name = customerDb.Name;

                _grid.CancelEditRow(customer);

                StateHasChanged();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task DeleteCustomer(CustomerGetDto customer)
        {
            try
            {
                var isConfirm = await ShowConfirm($"Czy napewno chcesz usunąć klienta {customer.Name}?");
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
            _customers.Add(Mapper.Map<CustomerGetDto>(customer));
            await _grid.Reload();

            window.Close();
            StateHasChanged();
        }
    }
}
