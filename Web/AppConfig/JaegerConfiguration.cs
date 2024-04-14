using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using OpenTracing;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;

namespace Web.AppConfig
{
    public static class JaegerConfiguration
    {
        //private IConfigCreator _configCreator;

        public static void AddJaeger(this IServiceCollection services)
        {

            var url =  SettingsConfigHelper.AppSetting("Jaeger:Host");
            var port = SettingsConfigHelper.AppSetting("Jaeger:Port");


            // Use "OpenTracing.Contrib.NetCore" to automatically generate spans for ASP.NET Core, Entity Framework Core, ...
            // See https://github.com/opentracing-contrib/csharp-netcore for details.
            services.AddOpenTracing();

            // private IConfigCreator _configCreator;

        // Adds the Jaeger Tracer.
        services.AddSingleton<ITracer>(serviceProvider =>
            {
                var serviceName = serviceProvider.GetRequiredService<IWebHostEnvironment>().ApplicationName;
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                // This is necessary to pick the correct sender, otherwise a NoopSender is used!
                Jaeger.Configuration.SenderConfiguration.DefaultSenderResolver = new SenderResolver(loggerFactory)
                    .RegisterSenderFactory<ThriftSenderFactory>();

                // This will log to a default localhost installation of Jaeger.
                var reporter = new RemoteReporter.Builder()
                .WithLoggerFactory(loggerFactory)
                //.WithSender(new UdpSender("35.213.138.77", 6831, 0))
                 .WithSender(new UdpSender(url, Convert.ToInt32(port), 0))
                .Build();

                var tracer = new Tracer.Builder(serviceName)
                    .WithSampler(new ConstSampler(true))
                    .WithLoggerFactory(loggerFactory)
                    .WithReporter(reporter)
                    .Build();

                // Allows code that can't use DI to also access the tracer.
                if (!GlobalTracer.IsRegistered())
                {
                    GlobalTracer.Register(tracer);
                }

                return tracer;
            });


            services.Configure<AspNetCoreDiagnosticOptions>(options =>
            {
                options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/status"));
                options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/metrics"));
                options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/swagger"));
            });
        }

        
    }

   
}
