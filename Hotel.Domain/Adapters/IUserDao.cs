using Hotel.Domain.Adapters.Common;
using Hotel.Domain.Entities;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters;

public interface IUserDao : IAddDao<User>, IGetDao<User>, ICountDao<User>
{
    Task<User> GetUserByTokenAsync(string token);
    Task<User> VerifyCredentialAsync(string email, string password);
    Task UpdateTokenAsync(int userId, string token);
}