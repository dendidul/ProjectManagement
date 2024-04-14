using Infrastructure.Helper.Config;
using Infrastructure.Helper.Encryption;
using Infrastructure.Helper.Auth;
using Application.Wrapper.Auth;
using Application.Repositories.UserApi;
using API.Utils;
using Application.Wrapper.Utility;

using Infrastructure.Repository;

using Infrastructure.Helper.Upload;
using Application.ExternalAPI.NewKalbeConnect;
using Infrastructure.WebServices;
using Infrastructure.Cache;

using Infrastructure.Helper.Notif;
using Infrastructure.Helper.Common;
using Application.ExternalAPI.LoyaltyAPI;


namespace API.AppConfig
{
    public static class AppDependency
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddSingleton<IConfigCreatorHelper, ConfigCreatorHelper>();
            services.AddSingleton<ISignature, Signature>();
            services.AddSingleton(typeof(IDataAccessClientRepository), typeof(DataAccessClientRepository));

            services.AddScoped<IAuthAccessToken, AuthAccessToken>();
            services.AddScoped<IStringEncryption, StringEncryption>();
            services.AddScoped<IAccessTokenHelper, AccessTokenHelper>();
            services.AddScoped<IPasswordEncryption, PasswordEncryption>();
            services.AddScoped<IValidateAccessToken, ValidateAccessToken>();
            services.AddScoped<IUserAPIDA, UserAPIDA>();
            services.AddScoped<AuthorizedAccessTokenAttribute>();
            services.AddScoped<IUtilityWrapper, UtilityWrapper>();
          
            services.AddScoped<IRedisCache, RedisCache>();
            services.AddTransient<INewKalbeConnectAPI, NewKalbeConnectAPI>();
            services.AddTransient<IKalbeConnectAPIService, KalbeConnectAPIService>();
            

            services.AddScoped<IUploadFilesHelper, UploadFilesHelper>();
      
            services.AddScoped<INotifEmail, NotifEmail>();
            services.AddScoped<ICommonHelper, CommonHelper>();
            services.AddScoped<ILoyaltyAPIService, LoyaltyAPIService>();
            services.AddScoped<ILoyaltyAPI, LoyaltyAPI>();

            return services;
        }
    }
}
