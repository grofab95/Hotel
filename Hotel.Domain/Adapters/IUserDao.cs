using Hotel.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IUserDao
    {
        Task<int> GetTotalAsync();
        Task<List<User>> GetAllAsync(int page, int limit);
        Task<User> GetUserByTokenAsync(string token);
        Task<User> VerifyCredentialAsync(string email, string password);
        Task UpdateTokenAsync(int userId, string token);
    }
}
