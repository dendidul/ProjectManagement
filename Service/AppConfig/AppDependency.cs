using Infrastructure.Helper.Config;
using Infrastructure.Helper.Encryption;
using Infrastructure.Helper.Auth;
using Application.Wrapper.Auth;
using Application.Repositories.UserApi;
//using Service.Utils;
using Application.Wrapper.Utility;

using Infrastructure.Repository;

using Infrastructure.Helper.Upload;
using Application.ExternalAPI.NewKalbeConnect;
using Infrastructure.WebServices;
using Infrastructure.Cache;

using Infrastructure.Helper.Notif;
using Infrastructure.Helper.Common;
using Application.ExternalAPI.LoyaltyAPI;

using Microsoft.Extensions.DependencyInjection;
using Application.Repositories.CustomerLink;
using Service.Executor;
using Application.Repositories.Notification;
using Application.Repositories.OrderTransaction;

namespace Service.AppConfig
{
    public static class AppDependency
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddSingleton<IConfigCreatorHelper, ConfigCreatorHelper>();
            services.AddSingleton<ISignature, Signature>();
            services.AddSingleton(typeof(IDataAccessClientRepository), typeof(DataAccessClientRepository));
            services.AddScoped<IFPRSBlastLogDA, FPRSBlastLogDA>();
            services.AddScoped<IAuthAccessToken, AuthAccessToken>();
            services.AddScoped<IStringEncryption, StringEncryption>();
            services.AddScoped<IAccessTokenHelper, AccessTokenHelper>();
            services.AddScoped<IPasswordEncryption, PasswordEncryption>();
            services.AddScoped<IValidateAccessToken, ValidateAccessToken>();
            services.AddScoped<IUserAPIDA, UserAPIDA>();
           // services.AddScoped<AuthorizedAccessTokenAttribute>();
            services.AddScoped<IUtilityWrapper, UtilityWrapper>();
            services.AddScoped<ICustomerLinkDA, CustomerLinkDA>();
            
            services.AddScoped<IRedisCache, RedisCache>();
            services.AddTransient<INewKalbeConnectAPI, NewKalbeConnectAPI>();
            services.AddTransient<IKalbeConnectAPIService, KalbeConnectAPIService>();

            services.AddHttpContextAccessor();
            services.AddScoped<Services>();
            services.AddScoped<IQueuedExecutor, QueuedExecutor>();
            services.AddScoped<INotificationDA, NotificationDA>();
            services.AddScoped<ILoyaltyAPI, LoyaltyAPI>();
            services.AddScoped<ICustomerLinkDA, CustomerLinkDA>();
            services.AddScoped<IQueuedExecutor, QueuedExecutor>();
            services.AddSingleton<IConfigCreatorHelper, ConfigCreatorHelper>();
            services.AddSingleton<ILoyaltyAPIService, LoyaltyAPIService>();
            services.AddSingleton<ISignature, Signature>();
            services.AddSingleton<IRedisCache, RedisCache>();
            services.AddSingleton<IStringEncryption, StringEncryption>();

            services.AddScoped<IUploadFilesHelper, UploadFilesHelper>();


            services.AddScoped<IOrderTransactionDA, OrderTransactionDA>();
         
            //services.AddScoped<INotifEmail, NotifEmail>();
            //services.AddScoped<ICommonHelper, CommonHelper>();
            //services.AddScoped<ILoyaltyAPIService, LoyaltyAPIService>();
            //services.AddScoped<ILoyaltyAPI, LoyaltyAPI>();

            //services.AddScoped<Services>();
         
            //services.AddScoped<ILoyaltyAPI, LoyaltyAPI>();
            //services.AddScoped<ICustomerLinkDA, CustomerLinkDA>();
            //services.AddScoped<IQueuedExecutor, QueuedExecutor>();

            return services;
        }
    }
}
