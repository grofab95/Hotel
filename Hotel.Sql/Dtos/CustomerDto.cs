using Hotel.Domain.Entities.Common;

namespace Hotel.Sql.Dtos
{
    internal class CustomerDto : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
