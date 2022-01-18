using Hotel.Domain.Entities;
using System.Linq;

namespace Hotel.Domain.Tests.Helpers;

public class GuestCreator
{
    private Reservation _reservation;
    private bool _isChild;
    private bool _isNewlyweds;
    private bool _orderedBreakfest;
    private decimal _priceForStay;

    public GuestCreator(Reservation reservation, decimal priceForStay)
    {
        _reservation = reservation;
        _priceForStay = priceForStay;
    }

    public GuestCreator MarkAsChild()
    {
        _isChild = true;
        return this;
    }

    public GuestCreator MarkAsNewlyweds()
    {
        _isNewlyweds = true;
        return this;
    }

    public GuestCreator MarkAsHavingOrderedBreakfest()
    {
        _orderedBreakfest = true;
        return this;
    }

    public GuestCreator SetPriceForStay(decimal priceForStay)
    {
        _priceForStay = priceForStay;
        return this;
    }

    public Guest GetGuest()
    {
        var reservationRoom = _reservation.ReservationRooms.First();
        _reservation.AddGuestToRoom(reservationRoom, "TestGuest", _isChild, _isNewlyweds, _orderedBreakfest, _priceForStay);
        return _reservation.GetGuests().First();
    }
}