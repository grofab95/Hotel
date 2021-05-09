using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;
using Hotel.Sql.ContextFactory;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class UserDao : BaseDao, IUserDao
    {
        public UserDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        { }

        public async Task<int> GetTotalAsync() => await context.Users.CountAsync();

        public async Task<List<User>> GetAllAsync(int page, int limit)
        {
            return await context.Users.Include(x => x.Token)
                .OrderBy(x => x.Id)
                .Pagging(page, limit)
                .ToListAsync();
        }

        public async Task<User> GetUserByTokenAsync(string token)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Token != null && x.Token.Value == token)
                ?? throw new HotelException("Użytkownik nie odnaleziony.");
        }

        public async Task UpdateTokenAsync(int userId, string token)
        {
            var updateQuery = @"UPDATE TOKENS SET VALUE = @value WHERE UserId = @userId";
            await context.Database.ExecuteSqlRawAsync(updateQuery, parameters: new object[]
            {
                new SqlParameter("@value", token),
                new SqlParameter("@userId", userId)
            });
        }

        public async Task<User> VerifyCredentialAsync(string email, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email)
                ?? throw new HotelException($"Email {email} nie został odnaleziony.");

            if (user.IsPasswordValid(password) == false)
                throw new HotelException("Podane hasło jest nieprawidłowe.");

            return user;
        }

        public async Task<User> AddUser(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }
    }
}