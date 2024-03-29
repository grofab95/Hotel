﻿using Hotel.Application.Dtos.RoomDtos;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents;

public partial class ReservationFactorsCollectorComponent
{
    [Inject] IRoomDao RoomDao { get; set; }
    [Parameter] public EventCallback<FindedRoomsFactors> OnFindedRooms { get; set; }
    [Parameter] public EventCallback<ReservationFactors> OnChange { get; set; }

    private ReservationFactors _reservationFactors;
    private bool _isRoomSearching;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var actualDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            _reservationFactors = new ReservationFactors
            {
                CheckIn = actualDate.AddDays(1),
                CheckOut = actualDate.AddDays(2),
                BookingAmount = 4
            };
        }
        catch (Exception ex)
        {
            await HandleException(ex);
        }
    }

    private async Task DataChanged() => await OnChange.InvokeAsync(_reservationFactors);

    private async Task SearchRooms(ReservationFactors reservationFactors)
    {
        await DoSafeAction(async () =>
        {
            _isRoomSearching = true;

            var findedRooms = await RoomDao.GetFreeByDateRangeAsync(reservationFactors.BookingAmount,
                new DateRange(reservationFactors.CheckIn, reservationFactors.CheckOut));

            _isRoomSearching = false;

            await OnFindedRooms.InvokeAsync(new FindedRoomsFactors(reservationFactors, findedRooms));
        });
    }

    private void OnCustomerSelected(Customer customer)
    {
        _reservationFactors.Customer = customer;

        window.Close();
    }
}