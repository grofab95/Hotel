using Hotel.Domain.Entities.Views;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Sql.Configurations
{
    static class ViewsConfiguration
    {
        public static void ApplyViews(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationInfoView>(x =>
            {
                x.HasNoKey();
                x.ToView("ReservationInfoView");
            });
        }
    }
}
