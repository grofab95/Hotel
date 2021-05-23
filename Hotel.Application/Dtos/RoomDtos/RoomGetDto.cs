using Hotel.Application.Dtos.AreaDtos;

namespace Hotel.Application.Dtos.RoomDtos
{
    public class RoomGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PeopleCapacity { get; set; }
        public AreaGetDto Area { get; set; }
    }
}
