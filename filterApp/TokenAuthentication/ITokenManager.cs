namespace filterApp.TokenAuthentication
{
    public interface ITokenManager
    {
        bool Authenticate(string userName, string password);
        Token CreateToken();
        bool VerifyToken(string token);
    }
}