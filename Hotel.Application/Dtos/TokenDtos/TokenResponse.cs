namespace Hotel.Application.Dtos.TokenDtos
{
    public class TokenResponse
    {
        public string Token { get; private set; }

        public TokenResponse(string token)
        {
            Token = token;
        }
    }
}
