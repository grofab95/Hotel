﻿using Hotel.Domain.Entities;
using Hotel.Domain.Entities.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IReservationDao
    {
        Task<int> SaveReservationAsync(Reservation reservation);
        Task<List<ReservationInfoView>> GetReservationBasicInfosAsync();
        //Task<List<ReservationInfoView>> SearchReservations(Expression<Func<ReservationInfoView, bool>> query);
        IQueryable<ReservationInfoView> SearchReservations();
        Task<Reservation> GetReservationByIdAsync(int reservationId);
    }
}