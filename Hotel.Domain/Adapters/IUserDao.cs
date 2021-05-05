using Hotel.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IUserDao
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetUserByTokenAsync(string token);
        Task<User> VerifyCredentialAsync(string email, string password);
        Task UpdateTokenAsync(int userId, string token);
    }
}
