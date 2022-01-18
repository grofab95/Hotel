using Hotel.Application.Dtos.AreaDtos;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Web.Components.AreaComponents;

public partial class AreasComponent
{
    [Inject] IAreaDao AreaDao { get; set; }

    private List<AreaGetDto> _areas;
    private AreaGetDto _newArea = new AreaGetDto();

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
        var areas = await AreaDao.GetManyAsync(1, 500, x => x.Id > 0);   // todo: implement paggination
        _areas = Mapper.Map<List<AreaGetDto>>(areas);
    }

    private async Task AddArea()
    {
        try
        {
            var createdArea = await AreaDao.AddAsync(new Area(_newArea.Name));

            await ShowNotification("Dodano pomyślnie", Radzen.NotificationSeverity.Success);

            _areas.Add(Mapper.Map<AreaGetDto>(createdArea));

            _newArea = new AreaGetDto();

            StateHasChanged();
        }
        catch (Exception ex)
        {
            await HandleException(ex);
        }
    }

    private async Task Delete(AreaGetDto area)
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