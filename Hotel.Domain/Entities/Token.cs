using Hotel.Domain.Entities.Common;

namespace Hotel.Domain.Entities
{
    public class Token : Entity
    {
        public string Value { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        public Token(User user) 
        {
            User = user;
        }

        private Token() { }
    }
}
