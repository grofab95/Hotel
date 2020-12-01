using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Extensions;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.AreaComponents
{
    public partial class AreasComponent
    {
        [Inject] IAreaDao AreaDao { get; set; }

        private List<AreaDto> _areas;
        private AreaDto _newArea = new AreaDto();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadAreas();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task LoadAreas()
        {
            var areas = await AreaDao.GetManyAsync(x => x.Id > 0);
            _areas = Mapper.Map<List<AreaDto>>(areas);
        }

        private async Task AddArea()
        {
            try
            {
                var createdArea = await AreaDao.AddAsync(new Area(_newArea.Name));

                await ShowNotification("Dodano pomyślnie", Radzen.NotificationSeverity.Success);

                _areas.Add(Mapper.Map<AreaDto>(createdArea));

                _newArea = new AreaDto();

                StateHasChanged();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task Delete(AreaDto area)
        {
            if (!(await ShowConfirm($"Czy napewno chcesz usunać {area.Name}?")))
                return;

            try
            {
                await AreaDao.DeleteAsync(area.Id);

                _areas.Remove(area);

                StateHasChanged();

                await ShowNotification("Usnięto pomyślnie", Radzen.NotificationSeverity.Success);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task Save()
        {
            try
            {
                foreach (var area in _areas)              
                    await AreaDao.UpdateAsync(Mapper.Map<Area>(area));

                await ShowNotification("Zapisano pomyślnie", Radzen.NotificationSeverity.Success);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task Cancel()
        {
            try
            {
                await LoadAreas();

                StateHasChanged();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }
}
