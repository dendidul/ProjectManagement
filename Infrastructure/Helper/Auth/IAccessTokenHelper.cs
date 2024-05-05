namespace Infrastructure.Helper.Auth
{
    public interface IAccessTokenHelper
    {
        string GetToken(string clientID, string clientApiKey, string clientApiSecret, long ticks = 0);

    }
}