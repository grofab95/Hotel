﻿using Hotel.Domain.Adapters;
using Hotel.Sql.Daos;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Web
{
    public static class ServicesRegistration
    {
        public static void AddDaos(this IServiceCollection services)
        {
            services.AddScoped<IPriceRuleDao, PriceRuleDao>();
            services.AddScoped<IReservationDao, ReservationDao>();
            services.AddScoped<IRoomDao, RoomDao>();
            services.AddScoped<ICustomerDao, CustomerDao>();
        }
    }
}
