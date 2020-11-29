using Hotel.Domain.Adapters;
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

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var areas = await AreaDao.GetManyAsync(x => x.Id > 0);
                _areas = Mapper.Map<List<AreaDto>>(areas);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }
}
