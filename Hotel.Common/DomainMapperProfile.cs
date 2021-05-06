using AutoMapper;
using Hotel.Application.Dtos;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;

namespace Hotel.Common
{
    public class DomainMapperProfile : Profile
    {
        public DomainMapperProfile()
        {
            CreateMap<PriceRule, PriceRuleDto>();
            CreateMap<PriceRuleDto, PriceRule>();

            CreateMap<Guest, GuestDto>();
            CreateMap<GuestDto, Guest>();

            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();

            CreateMap<Area, AreaDto>();
            CreateMap<AreaDto, Area>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
