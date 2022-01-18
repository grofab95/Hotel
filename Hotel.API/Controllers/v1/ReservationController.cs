using AutoMapper;
using Hotel.Api.Wrappers;
using Hotel.Application.Dtos.ReservationDtos;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Environment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Api.Controllers.v1;

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

    [Route("{reservationId}/room/addRoom/{roomId}")]
    [HttpPut]
    public async Task<IActionResult> AddRoomToReservationAsync(int reservationId, int roomId)
    {
        await _reservationDao.AddRoomToReservationAsync(reservationId, roomId);
        return Ok(new SuccessResponse());
    }

    [Route("{reservationId}/room/removeRoom/{roomId}")]
    [HttpPut]
    public async Task<IActionResult> RemoveRoomToReservationAsync(int reservationId, int roomId)
    {
        await _reservationDao.RemoveRoomFromReservationAsync(reservationId, roomId);
        return Ok(new SuccessResponse());
    }

    [Route("{reservationId}/guest/addGuest/{guestId}")]
    [HttpPut]
    public async Task<IActionResult> AddGuestToReservationRoomAsync(int reservationId, int guestId)
    {
        await _reservationDao.AddGuestToReservationRoomAsync(reservationId, guestId);
        return Ok(new SuccessResponse());
    }

    [Route("{reservationId}/guest/removeGuest/{guestId}")]
    [HttpPut]
    public async Task<IActionResult> RemoveGuestFromReservationRoomAsync(int reservationId, int guestId)
    {
        await _reservationDao.RemoveGuestFromReservationRoomAsync(reservationId, guestId);
        return Ok(new SuccessResponse());
    }

    [Route("{reservationId}/price/total")]
    [HttpGet]
    public async Task<IActionResult> GetReservationPriceAsync(int reservationId)
    {
        var totalPrice = await _reservationDao.GetReservationPriceAsync(reservationId);
        return Ok(new SuccessResponse<ReservationPriceGetDto>(new ReservationPriceGetDto(reservationId, totalPrice)));
    }

    [Route("{reservationId}/price/forDay")]
    [HttpGet]
    public async Task<IActionResult> GetReservationPriceForDay(int reservationId)
    {
        var priceForDay = await _reservationDao.GetReservationPriceForDay(reservationId);
        return Ok(new SuccessResponse<ReservationPriceGetDto>(new ReservationPriceGetDto(reservationId, priceForDay)));
    }
}