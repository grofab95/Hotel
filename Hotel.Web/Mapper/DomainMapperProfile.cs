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
        }
    }
}
