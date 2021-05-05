using Blazored.LocalStorage;
using Hotel.Application.Managers;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Web.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hotel.Web.Providers
{
    public class AuthenticationProvider : AuthenticationStateProvider
    {
        private const string TOKEN = "token";
        private ILocalStorageService _localStorage;
        private IUserDao _userDao;

        public AuthenticationProvider(ILocalStorageService localStorage, IUserDao userDao)
        {
            _localStorage = localStorage;
            _userDao = userDao;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>(TOKEN);
                if (token == default)
                    return EmptyState;

                if (!TokenManager.IsTokenValid(token, out JwtSecurityToken jwtSecurityToken))
                {
                    await _localStorage.RemoveItemAsync(TOKEN);
                    return EmptyState;
                }

                var dbUser = await _userDao.GetUserByTokenAsync(token);
                if (dbUser == null)
                {
                    await _localStorage.RemoveItemAsync(TOKEN);
                    return EmptyState;
                }

                var claims = BuildClaims(dbUser, token);
                return new AuthenticationState(new ClaimsPrincipal(claims));
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Lifetime validation failed. The token is expired."))
                    Log.Error(ex.Message);

                await _localStorage.RemoveItemAsync(TOKEN);
                return EmptyState;
            }
        }

        public AuthenticationState EmptyState => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public async Task MarkUserAsAuthenticated(LoginDto loginDto)
        {
            var dbUser = await _userDao.VerifyCredentialAsync(loginDto.Email, loginDto.Password);
            var token = TokenManager.GnerateToken(dbUser);
            await _localStorage.SetItemAsync(TOKEN, token);
            await _userDao.UpdateTokenAsync(dbUser.Id, token);
            var claims = BuildClaims(dbUser, token);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(claims))));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync(TOKEN);
            NotifyAuthenticationStateChanged(Task.FromResult(EmptyState));
        }

        private ClaimsIdentity BuildClaims(User user, string token)
        {
            return new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(TOKEN, token)
            }, "apiauth_type");
        }
    }
}
