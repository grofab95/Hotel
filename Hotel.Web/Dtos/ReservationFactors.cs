using Hotel.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Web.Dtos
{
    //public class ReservationFactors
    //{
    //    public Reservation Reservation { get; set; }
    //    public Dictionary<int, List<GuestDto>> RoomIdGuests { get; set; }
    //    public int BookingAmount { get; set; }

    //    public ReservationFactors(Reservation reservation, int bookingAmount)
    //    {
    //        Reservation = reservation;
    //        BookingAmount = bookingAmount;
    //        RoomIdGuests = new Dictionary<int, List<GuestDto>>();
    //    }

    //    public void AddRoom(Room room)
    //    {
    //        Reservation.AddRoom(room);
    //        RoomIdGuests.Add(room.Id, new List<GuestDto>());
    //    }

    //    public void DeleteRoom(Room room)
    //    {
    //        Reservation.DeleteRoom(room);
    //        RoomIdGuests.Remove(room.Id);
    //    }

    //    public int PeopleAmount => RoomIdGuests.SelectMany(x => x.Value).Count();
    //    public decimal TotalCost => RoomIdGuests.SelectMany(x => x.Value).Sum(x => x.PriceForStay);
    //}
}
