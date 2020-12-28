namespace Hotel.Application.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PeopleCapacity { get; set; }
        public AreaDto Area { get; set; }
    }
}
