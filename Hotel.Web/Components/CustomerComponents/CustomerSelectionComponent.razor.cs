using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Web.Components.CustomerComponents;

public partial class CustomerSelectionComponent
{
    [Inject] ICustomerDao CustomerDao { get; set; }
    [Parameter] public Customer AlreadySlectedCustomer { get; set; }
    [Parameter] public EventCallback<Customer> OnAccepted { get; set; }

    private List<Customer> _findedCustomers;
    private Customer _selectedCustomer;

    private string _searchedValues;

    protected override async Task OnParametersSetAsync()
    {
        if (AlreadySlectedCustomer != null)
            _selectedCustomer = AlreadySlectedCustomer;

        //_findedCustomers = await CustomerDao.GetManyAsync(x => x.Id > 0);
    }

    private async Task SearchCustomers()
    {
        try
        {
            if (_searchedValues.IsNotExist())
                return;

            _findedCustomers = await CustomerDao.GetManyAsync(1, 1000, x => x.Name.Contains(_searchedValues));  // todo: implement paggination

            StateHasChanged();
        }
        catch (Exception ex)
        {
            await HandleException(ex);
        }
    }

    private void HandleFindedCustomerCheck(Customer customer)
    {
        _selectedCustomer = customer;
    }

    private async Task OnSearchedValueTyped(KeyboardEventArgs args)
    {
        if (args.Code == "Enter")
            await SearchCustomers();
    }

    private void OnCustomerCreated(Customer customer)
    {
        _selectedCustomer = customer;

        window.Close();

        StateHasChanged();
    }

    private async Task CustomerAccepted()
    {
        await OnAccepted.InvokeAsync(_selectedCustomer);
    }
}