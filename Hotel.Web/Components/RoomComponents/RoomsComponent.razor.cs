using Hotel.Application.Dtos.AreaDtos;
using Hotel.Application.Dtos.RoomDtos;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
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

        private List<RoomGetDto> _rooms;
        private List<AreaGetDto> _areas;
        private RadzenGrid<RoomGetDto> _grid;

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

        public async Task LoadData() // todo: implement paggination
        {
            _rooms = Mapper.Map<List<RoomGetDto>>(await RoomDao.GetManyAsync(1, 500, x => x.Id > 0));
            _areas = Mapper.Map<List<AreaGetDto>>(await AreaDao.GetManyAsync(1, 500, x => x.Id > 0));
        }

        void EditRow(RoomGetDto room)
        {
            _grid.EditRow(room);
        }

        private async Task SaveRow(RoomGetDto room)
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

        private async Task CancelEdit(RoomGetDto room)
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
                room.Area = Mapper.Map<AreaGetDto>(roomDb.Area);
                room.PeopleCapacity = roomDb.PeopleCapacity;

                _grid.CancelEditRow(room);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task DeleteRow(RoomGetDto room)
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
            var newRoom = new RoomGetDto();
            _rooms.Add(newRoom);
            await _grid.Reload();
            await _grid.EditRow(newRoom);            
        }

        private async Task AddRow(RoomGetDto room) 
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
