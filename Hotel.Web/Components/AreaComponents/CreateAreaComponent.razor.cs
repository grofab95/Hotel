using Hotel.Application.Dtos;
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

        private AreaDto _newArea = new AreaDto();

        private async Task AddArea(AreaDto areaDto)
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
