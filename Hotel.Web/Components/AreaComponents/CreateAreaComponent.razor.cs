using Hotel.Application.Dtos.AreaDtos;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Hotel.Web.Components.AreaComponents
{
    public partial class CreateAreaComponent
    {
        [Inject] IAreaDao AreaDao { get; set; }

        private AreaGetDto _newArea = new AreaGetDto();

        private async Task AddArea(AreaGetDto areaDto)
        {
            try
            {
                await AreaDao.AddAsync(Mapper.Map<Area>(areaDto));
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }
}
