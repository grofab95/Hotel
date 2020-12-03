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
        [Parameter] public List<Area> Areas { get; set; }

        private List<RoomDto> _rooms;
        private List<AreaDto> _areas;
        private RadzenGrid<RoomDto> _grid;
        private RoomDto _room;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadData();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        public async Task LoadData()
        {
            _rooms = Mapper.Map<List<RoomDto>>(await RoomDao.GetManyAsync(x => x.Id > 0));
            _areas = Mapper.Map<List<AreaDto>>(await AreaDao.GetManyAsync(x => x.Id > 0));
        }

        void EditRow(RoomDto room)
        {
            _grid.EditRow(room);
        }

        private async Task SaveRow(RoomDto room)
        {
            try
            {
                if (room.Id == 0)
                {
                    await AddRow(room);

                    _grid.CancelEditRow(room);
                    return;
                }

                var updatedRoom = Mapper.Map<Room>(room);
                updatedRoom.Update(Mapper.Map<Area>(room.Area), room.Name, room.PeopleCapacity);
                await RoomDao.UpdateAsync(updatedRoom);

                await ShowNotification("Zapisano pomyślnie", Radzen.NotificationSeverity.Success);
                _grid.CancelEditRow(room);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task CancelEdit(RoomDto room)
        {
            if (room.Id == 0)
            {
                _rooms.Remove(room);
                await _grid.Reload();
                return;
            }

            try
            {
                var roomDb = await RoomDao.GetAsync(x => x.Id == room.Id);
                room.Name = roomDb.Name;
                room.Area = Mapper.Map<AreaDto>(roomDb.Area);
                room.PeopleCapacity = roomDb.PeopleCapacity;

                _grid.CancelEditRow(room);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task DeleteRow(RoomDto room)
        {
            if (room.Id == 0)
            {
                _rooms.Remove(room);
                await _grid.Reload();

                return;
            }

            if (!(await ShowConfirm($"Czy napewno chcesz usunać pokój {room.Name}?")))
                return;

            try
            {
                await RoomDao.DeleteAsync(room.Id);
                _rooms.Remove(room);
                await _grid.Reload();

                await ShowNotification("Usunięto pomyślnie", Radzen.NotificationSeverity.Success);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task InsertRow()
        {
            var newRoom = new RoomDto();
            _rooms.Add(newRoom);
            await _grid.Reload();
            await _grid.EditRow(newRoom);            
        }

        private async Task AddRow(RoomDto room) 
        {
            var area = Mapper.Map<Area>(room.Area);
            var added = await RoomDao.AddAsync(new Room(area, room.Name, room.PeopleCapacity));

            room.Id = added.Id;

            await _grid.Reload();
            StateHasChanged();

            await ShowNotification("Dodano pomyślnie", Radzen.NotificationSeverity.Success);
        }
    }
}
