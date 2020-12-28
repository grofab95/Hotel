using Hotel.Domain.Adapters;
using Hotel.Sql.Daos;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Web
{
    public static class ServicesRegistrator
    {
        public static void AddDaos(this IServiceCollection services)
        {
            services.AddScoped<IPriceRuleDao, PriceRuleDao>();
            services.AddScoped<IReservationDao, ReservationDao>();
            services.AddScoped<IRoomDao, RoomDao>();
            services.AddScoped<IAreaDao, AreaDao>();
            services.AddScoped<ICustomerDao, CustomerDao>();
            services.AddScoped<IStatisticDao, StatisticDao>();
        }
    }
}
