using System;

namespace Hotel.Application.Dtos.ReservationDtos;

public class ReservationCreateDto
{
    public int CustomerId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}