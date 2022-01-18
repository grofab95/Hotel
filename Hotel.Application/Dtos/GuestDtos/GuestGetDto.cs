namespace Hotel.Application.Dtos.GuestDtos;

public class GuestGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsChild { get; set; }
    public bool IsNewlyweds { get; set; }
    public bool OrderedBreakfest { get; set; }
    public decimal BasePrice { get; set; }
    public decimal PriceForStay { get; set; }
}