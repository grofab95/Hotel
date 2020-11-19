using Hotel.Domain.Entities.Common;

namespace Hotel.Sql.Dtos
{
    internal class RoomDto : Entity
    {
        public string Name { get; set; }
        public int PeopleCapacity { get; set; }
        public AreaDto Area { get; set; }
    }
}
