using Infrastructure.Helper.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.AppConfig;
using Service.Executor;
using Application.ExternalAPI.LoyaltyAPI;
using Application.Repositories.CustomerLink;
using Infrastructure.WebServices;
using Infrastructure.Helper.Encryption;
using Infrastructure.Cache;

namespace Service
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();


            var host = new HostBuilder()
                  .ConfigureAppConfiguration((hostContext, config) =>
                  {
                      config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                      config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", reloadOnChange: true, optional: true);
                      config.AddEnvironmentVariables(prefix: "PREFIX_");

                      if (args != null)
                      {
                          config.AddCommandLine(args);
                      }

                    

                  })
                .ConfigureServices((hostContext, services) =>
                {
                   
                    //services.AddHttpContextAccessor();
                    //services.AddScoped<Services>();
                    //services.AddScoped<IQueuedExecutor, QueuedExecutor>();
                    //services.AddScoped<ILoyaltyAPI, LoyaltyAPI>();
                    //services.AddScoped<ICustomerLinkDA, CustomerLinkDA>();
                    //services.AddScoped<IQueuedExecutor, QueuedExecutor>();
                    //services.AddSingleton<IConfigCreatorHelper, ConfigCreatorHelper>();
                    //services.AddSingleton<ILoyaltyAPIService, LoyaltyAPIService>();
                    //services.AddSingleton<ISignature, Signature>();
                    //services.AddSingleton<IRedisCache, RedisCache>();
                    //services.AddSingleton<IStringEncryption, StringEncryption>();
                    services.AddDependency();
                    //services.AddJaegerTracing(configuration);
                   // services.AddJaeger();
                    //              private ILoyaltyAPI _loyaltyAPI;
                    //private ICustomerLinkDA _customerLinkDA;

                }).UseConsoleLifetime()
                .Build();

            using (host)
            {
                await host.StartAsync();

                var processLoop = host.Services.GetRequiredService<Services>();
                processLoop.Start();

                await host.WaitForShutdownAsync();
            }
        }

    }
}