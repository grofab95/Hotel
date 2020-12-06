//using Hotel.Domain.Adapters;
//using Hotel.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Hotel.Application.Services
//{
//    public class RoomFinderService
//    {
//        private IReservationRoomDao _reservationRoomDao;
//        private IRoomDao _roomDao;

//        private int _peopleAmount;
//        private DateRange _dateRange;

//        private List<Room> _findedRooms;
//        private List<Room> _allRooms;

//        public RoomFinderService(IReservationRoomDao reservationRoomDao, IRoomDao roomDao, int peopleAmount, DateRange dateRange)
//        {
//            _reservationRoomDao = reservationRoomDao;
//            _roomDao = roomDao;
//            _peopleAmount = peopleAmount;
//            _dateRange = dateRange;
//            //_allRooms = 
//        }

//        public async Task<List<Room>> GetRooms()
//        {
//            var reservatedRooms = GetReservatedRooms();

//            return null;
//        }

//        private async Task<List<KeyValuePair<Room, DateTime>>> GetReservatedRooms()
//        {
//            var reservatedRooms = await _reservationRoomDao.GetReservatedByDates(_dateRange);

//            return reservatedRooms
//                .Select(x => new KeyValuePair<Room, DateTime>(x.Room, x.Reservation.CheckOut))
//                .ToList();
//        }

//        private List<Room> SelectRooms(List<Room> rooms)
//        {
//            var selectedRooms = new List<Room>();
         
//            foreach (var room in rooms.OrderByDescending(x => x.PeopleCapacity))
//            {
//                var allocatedAmount = selectedRooms.Sum(x => x.PeopleCapacity);
//                var missingAmount = Math.Max(0, _peopleAmount - allocatedAmount);
//                if (missingAmount == 0)
//                    break;

//                //if (room.PeopleCapacity)
//            }

//            return selectedRooms;
//        }
//    }
//}
