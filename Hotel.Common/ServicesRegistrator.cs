using AutoMapper;
using Hotel.Application.Infrastructure;
using Hotel.Domain.Adapters;
using Hotel.Domain.Environment;
using Hotel.Sql;
using Hotel.Sql.ContextFactory;
using Hotel.Sql.Daos;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hotel.Common;

public static class ServicesRegistrator
{
    public static void RegisterDaos(this IServiceCollection services)
    {
        services.AddScoped<IPriceRuleDao, PriceRuleDao>();
        services.AddScoped<IReservationDao, ReservationDao>();
        services.AddScoped<IRoomDao, RoomDao>();
        services.AddScoped<IAreaDao, AreaDao>();
        services.AddScoped<ICustomerDao, CustomerDao>();
        services.AddScoped<IStatisticDao, StatisticDao>();
        services.AddScoped<IUserDao, UserDao>();
    }

    public static void RegisterLogger(this IServiceCollection services)
    {
        services.AddScoped<ILogger, Logger>();
    }

    public static void RegisterContextFactory(this IServiceCollection services)
    {
        services.AddContextFactory<HotelContext>();
    }

    public static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DomainMapperProfile).GetTypeInfo().Assembly);
    }
}