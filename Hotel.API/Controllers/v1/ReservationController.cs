using AutoMapper;
using Hotel.API.Wrappers;
using Hotel.Application.Dtos.ReservationDtos;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Environment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetReservationsAsync([FromQuery] PaggedRequest paggedRequest)
        {
            var total = await _reservationDao.CountAsync(x => x.Id > 0);
            var reservations = await _reservationDao.GetManyAsync(paggedRequest.Page, paggedRequest.Size, x => x.Id > 0);
            var mapped = _mapper.Map<List<ReservationGetDto>>(reservations);
            return Ok(new PagedResponse<List<ReservationGetDto>>(mapped, total, paggedRequest));
        }

        [Route("getReservations/customer/{customerId}")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerReservationsAsync(int customerId, [FromQuery] PaggedRequest paggedRequest)
        {
            var total = await _reservationDao.CountAsync(x => x.Customer.Id == customerId);
            if (total == 0)
                return NotFound(new ErrorResponse("Klient nie posiada rezerwacji."));

            var reservations = await _reservationDao.GetManyAsync(paggedRequest.Page, paggedRequest.Size, x => x.Customer.Id == customerId);
            var mapped = _mapper.Map<List<ReservationGetDto>>(reservations);
            return Ok(new PagedResponse<List<ReservationGetDto>>(mapped, total, paggedRequest));
        }

        [Route("createReservation")]
        [HttpPost]
        public async Task<IActionResult> CreateReservationAsync([FromBody] ReservationCreateDto createDto)
        {
            var reservation = await _reservationDao.CreateReservation(createDto.CustomerId, new DateRange(createDto.CheckIn, createDto.CheckOut));
            return Ok(new SuccessResponse<Reservation>(reservation));
        }
    }
}
