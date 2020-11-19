using AutoMapper;
using Hotel.Domain.Entities;

namespace Hotel.Sql.Dtos.Mapper
{
    public class DomainMapperProfile : Profile
    {
        public DomainMapperProfile()
        {
            CreateMap<Guest, GuestDto>();
            CreateMap<GuestDto, Guest>();

            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, Reservation>();

            CreateMap<ReservationRoom, ReservationRoomDto>();
            CreateMap<ReservationRoomDto, ReservationRoom>();

            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();

            CreateMap<Area, AreaDto>();
            CreateMap<AreaDto, Area>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
