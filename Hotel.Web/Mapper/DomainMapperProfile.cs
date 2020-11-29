using AutoMapper;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Web.Dtos;

namespace Hotel.Web.Mapper
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
