namespace Hotel.Application.Dtos.ReservationDtos;

public class ReservationPriceGetDto
{
    public int ReservationId { get; set; }
    public decimal Price { get; set; }

    public ReservationPriceGetDto(int reservationId, decimal price)
    {
        ReservationId = reservationId;
        Price = price;
    }
}