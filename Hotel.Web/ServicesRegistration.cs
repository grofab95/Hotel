using Hotel.Domain.Adapters;
using Hotel.Sql.Daos;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Web
{
    public static class ServicesRegistration
    {
        public static void AddDaos(this IServiceCollection services)
        {
            services.AddScoped<IPriceRuleDao, PriceRuleDao>();
            services.AddScoped<IRoomDao, RoomDao>();
        }
    }
}
