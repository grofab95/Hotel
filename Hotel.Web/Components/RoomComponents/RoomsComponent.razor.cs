using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Web.Components.RoomComponents
{
    public partial class RoomsComponent
    {
        [Inject] IRoomDao RoomDao { get; set; }
        [Inject] IAreaDao AreaDao { get; set; }

        private List<RoomDto> _rooms;
        private List<AreaDto> _areas;
        private RadzenGrid<RoomDto> _grid;
        private RoomDto _room;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _rooms = Mapper.Map<List<RoomDto>>(await RoomDao.GetManyAsync(x => x.Id > 0));
                _areas = Mapper.Map<List<AreaDto>>(await AreaDao.GetManyAsync(x => x.Id > 0));
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        void EditRow(RoomDto room)
        {
            _grid.EditRow(room);
        }

        private async Task OnUpdateRow(RoomDto room)
        {
            try
            {
                await RoomDao.UpdateAsync(Mapper.Map<Room>(room));

                await ShowNotification("Zapisano pomyślnie", Radzen.NotificationSeverity.Success);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private void SaveRow(RoomDto room)
        {
            _grid.UpdateRow(room);
        }

        private async Task CancelEdit(RoomDto room)
        {
            _grid.CancelEditRow(room);

            try
            {
                var roomDb = await RoomDao.GetAsync(x => x.Id == room.Id);
                room.Name = roomDb.Name;
                room.Area = Mapper.Map<AreaDto>(roomDb.Area);
                room.PeopleCapacity = roomDb.PeopleCapacity;
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task DeleteRow(RoomDto room)
        {
            if (!(await ShowConfirm($"Czy napewno chcesz usunać pokój {room.Name}?")))
                return;

            try
            {
                await RoomDao.DeleteAsync(room.Id);

                await ShowNotification("Usunięto pomyślnie", Radzen.NotificationSeverity.Success);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private void InsertRow()
        {
            _grid.InsertRow(new RoomDto());
        }

        private async Task OnCreateRow(RoomDto room) 
        {
            try
            {
                await RoomDao.AddAsync(Mapper.Map<Room>(room));

                await ShowNotification("Dodano pomyślnie", Radzen.NotificationSeverity.Success);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }
}
