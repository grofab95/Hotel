using AutoMapper;
using Hotel.Application.Dtos.AreaDtos;
using Hotel.Application.Dtos.CustomerDtos;
using Hotel.Application.Dtos.GuestDtos;
using Hotel.Application.Dtos.PriceRuleDtos;
using Hotel.Application.Dtos.RoomDtos;
using Hotel.Application.Dtos.UserDtos;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;

namespace Hotel.Common
{
    public class DomainMapperProfile : Profile
    {
        public DomainMapperProfile()
        {
            CreateMap<PriceRule, PriceRuleGetDto>();
            CreateMap<PriceRuleGetDto, PriceRule>();

            CreateMap<Guest, GuestGetDto>();
            CreateMap<GuestGetDto, Guest>();

            CreateMap<Room, RoomGetDto>();
            CreateMap<RoomGetDto, Room>();

            CreateMap<Area, AreaGetDto>();
            CreateMap<AreaGetDto, Area>();

            CreateMap<Customer, CustomerGetDto>();
            CreateMap<CustomerGetDto, Customer>();

            CreateMap<User, UserGetDto>();
            CreateMap<UserGetDto, User>();
        }
    }
}
