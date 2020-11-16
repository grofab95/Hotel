using Hotel.Domain.Entities.Common;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Validators;

namespace Hotel.Domain.Entities
{
    public class RoomGuest : Entity
    {
        public string Name { get; private set; }
        public bool IsChild { get; private set; }
        public bool IsNewlyweds { get; private set; }
        public bool OrderedBreakfest { get; private set; }
        public decimal? PriceForStay { get; private set; }

        protected RoomGuest() { }

        internal RoomGuest(string name, bool isChild, bool isNewlyweds, bool orderedBreakfest,
            decimal? priceForStay = null)
        {
            RoomGuestValidators.ValidIfNameExist(name);
            RoomGuestValidators.ValidPriceForStay(priceForStay);

            Name = name;
            IsChild = isChild;
            IsNewlyweds = isNewlyweds;
            OrderedBreakfest = orderedBreakfest;
            PriceForStay = priceForStay;
        }

        //internal static RoomGuest Create(string name, bool isChild, bool isNewlyweds, bool orderedBreakfest,
        //    decimal? priceForStay = null)
        //{
        //    RoomGuestValidators.ValidName(name);
        //    RoomGuestValidators.ValidPriceForStay(priceForStay);

        //    return new RoomGuest
        //    {
        //        Name = name,
        //        IsChild = isChild,
        //        IsNewlyweds = isNewlyweds,
        //        OrderedBreakfest = orderedBreakfest,
        //        PriceForStay = priceForStay
        //    };
        //}

        internal RoomGuest Update(RoomGuest updatedRoomGuest)
        {
            RoomGuestValidators.ValidIfNameExist(updatedRoomGuest.Name);
            RoomGuestValidators.ValidPriceForStay(updatedRoomGuest.PriceForStay);

            if (updatedRoomGuest.Id != Id)
                throw new HotelException("Nie można przypisać gościa do innego pokoju");

            Name = updatedRoomGuest.Name;
            IsChild = updatedRoomGuest.IsChild;
            IsNewlyweds = updatedRoomGuest.IsNewlyweds;
            OrderedBreakfest = updatedRoomGuest.OrderedBreakfest;
            PriceForStay = updatedRoomGuest.PriceForStay;

            return this;
        }

        public override string ToString() => Name;
    }
}
