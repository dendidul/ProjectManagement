using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Service.AppConfig
{
    public static class JaegerTracer
    {
        public static IServiceCollection AddJaegerTracing(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenTelemetryTracing((builder) =>
            {
                builder.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation(option =>
                    {
                        option.Enrich = (activity, eventName, rawObject) =>
                        {
                            if (eventName.Equals("OnStartActivity"))
                            {
                                if (rawObject is HttpRequestMessage request && configuration.GetSection("OpenTelemetry:IncludeHttpRequest").Get<bool>())
                                {
                                    activity.SetTag("Headers-Request", request.Headers);
                                    activity.SetTag("Content-Request", request.Content != null ? request.Content.ReadAsStringAsync().Result : "");
                                }
                            }
                            else if (eventName.Equals("OnStopActivity") && configuration.GetSection("OpenTelemetry:IncludeHttpResponse").Get<bool>())
                            {
                                if (rawObject is HttpResponseMessage response)
                                {
                                    activity.SetTag("Headers-Response", response.Headers);
                                    activity.SetTag("Content-Response", response.Content.ReadAsStringAsync().Result);

                                }
                            }
                            else if (eventName.Equals("OnException") && configuration.GetSection("OpenTelemetry:IncludeException").Get<bool>())
                            {
                                if (rawObject is Exception exception)
                                {
                                    activity.SetTag("stackTrace", exception.StackTrace);
                                    activity.SetTag("errorMessage", exception.Message);
                                }
                            }
                        };
                    })
                    .AddEntityFrameworkCoreInstrumentation(option =>
                    {
                        if (configuration.GetSection("OpenTelemetry:IncludeSqlQuery").Get<bool>())
                        {
                            option.SetDbStatementForText = true;
                            option.SetDbStatementForStoredProcedure = true;
                        }
                    })
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService($"{configuration.GetSection("ServiceName:Id").Value}_{configuration.GetSection("ServiceName:Name").Value}", null, configuration.GetSection("ServiceName:Version").Value))
                    .AddJaegerExporter(opts =>
                    {
                        opts.AgentHost = configuration.GetSection("OpenTelemetry:Tracing:Jaeger:AgentHost").Value;
                        opts.AgentPort = Convert.ToInt32(configuration.GetSection("OpenTelemetry:Tracing:Jaeger:AgentPort").Value);
                        opts.ExportProcessorType = ExportProcessorType.Batch;
                        if (configuration.GetSection("OpenTelemetry:ExportProcessorType").Value == "Simple")
                        {
                            opts.ExportProcessorType = ExportProcessorType.Simple;
                        }

                    });
            });

            return services;
        }
    }
}
