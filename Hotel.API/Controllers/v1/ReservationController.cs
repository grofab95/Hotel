using AutoMapper;
using Hotel.API.Wrappers;
using Hotel.Application.Dtos.ReservationDtos;
using Hotel.Domain.Adapters;
using Hotel.Domain.Environment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.API.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private IReservationDao _reservationDao;
        private ILogger _logger;
        private IMapper _mapper;

        public ReservationController(IReservationDao reservationDao, ILogger logger, IMapper mapper)
        {
            _reservationDao = reservationDao;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("getReservations")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaggedRequest paggedRequest)
        {
            try
            {
                var total = await _reservationDao.GetTotalAsync();
                var reservations = await _reservationDao.GetAllAsync(paggedRequest.Page, paggedRequest.Size);
                var mapped = _mapper.Map<List<ReservationGetDto>>(reservations);
                return Ok(new PagedResponse<List<ReservationGetDto>>(mapped, total, paggedRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(ex));
            }
        }
    }
}
