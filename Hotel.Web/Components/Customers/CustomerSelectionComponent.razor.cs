using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.Customers
{
    public partial class CustomerSelectionComponent
    {
        [Inject] ICustomerDao CustomerDao { get; set; }

        private List<Customer> _findedCustomers;
        private Customer _selectedCustomer;

        private string _searchedValues;

        private async Task SearchCustomers()
        {
            try
            {
                if (_searchedValues.IsNotExist())
                    return;

                _findedCustomers = await CustomerDao
                    .GetAsync(x => x.FirstName.Contains(_searchedValues) || x.LastName.Contains(_searchedValues));

                StateHasChanged();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }
}
