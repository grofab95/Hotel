using Hotel.Domain.Entities.Common;
using Hotel.Domain.Excetions;
using Hotel.Domain.Utilities;
using Hotel.Domain.Validators;
using System;

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

        internal static Result<RoomGuest> Create(string name, bool isChild, bool isNewlyweds, bool orderedBreakfest,
            decimal? priceForStay = null)
        {
            try
            {
                RoomGuestValidators.ValidName(name);
                RoomGuestValidators.ValidPriceForStay(priceForStay);

                return Result<RoomGuest>.Ok(new RoomGuest
                {
                    Name = name,
                    IsChild = isChild,
                    IsNewlyweds = isNewlyweds,
                    OrderedBreakfest = orderedBreakfest,
                    PriceForStay = priceForStay
                });
            }
            catch (Exception ex)
            {
                return Result<RoomGuest>.Fail(ex.Message);
            }         
        }

        internal RoomGuest Update(RoomGuest updatedRoomGuest)
        {
            RoomGuestValidators.ValidName(updatedRoomGuest.Name);
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
