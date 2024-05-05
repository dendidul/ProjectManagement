using Core.Dto.User;

namespace Application.Repositories.UserApi
{
    public interface IUserAPIDA
    {
        UserAPI AuthorizedUrlByAPIKeyAndSecret(string apikey, string secretkey, string url_method);
        UserAPI AuthorizeUrlByAPIKey(string apikey, string url_method);
        UserAPI GetUserApi(string apikey);
    }
}