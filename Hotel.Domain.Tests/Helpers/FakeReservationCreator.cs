using Hotel.Domain.Entities;
using System;
using System.Linq;

namespace Hotel.Domain.Tests.Helpers;

public class FakeReservationCreator
{
    private static readonly decimal _priceForStay = 50;
    private Reservation _reservation;
    private ReservationRoom _lastReservationRoom;
    private int _lastRoomId;
    private int _lastGuestId;

    public FakeReservationCreator(DateTime? checkIn = null, DateTime? checkOut = null)
    {
        var customer = new Customer("CustomerX");
        _reservation = Reservation.Create(
            customer, 
            checkIn ?? DateTime.Now.AddDays(1),
            checkOut ?? DateTime.Now.AddDays(5));
    }

    public Reservation GetReservation() => _reservation;

    public FakeReservationCreator AddRoom()
    {
        var room = new Room(new Area("Budynek A"), $"Pokój X{_lastRoomId}", 4);
        _lastRoomId++;
        _reservation.AddRoom(room);
        _lastReservationRoom = _reservation.ReservationRooms.Last();
        return this;
    }

    public FakeReservationCreator AddGuest()
    {
        var room = _reservation.GetRooms()?.FirstOrDefault();
        if (room == null)
        {
            AddRoom();
            room = _reservation.GetRooms()?.FirstOrDefault();
        }
        _reservation.AddGuestToRoom(_lastReservationRoom, $"Guest Y{_lastGuestId}", false, false, false, _priceForStay);
        _lastGuestId++;
        return this;
    }
}