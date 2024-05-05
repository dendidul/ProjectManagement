using Web.AppConfig;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Web
{         
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.Services.AddDbContext<ProjectManagementEntities>(options =>
            //options.UseNpgsql(builder.Configuration.GetConnectionString("ProjectManagement")));

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.RegisterConnections(builder.Configuration);

            builder.Services.AddCors();
            builder.Services.AddDependency();

            builder.Services.AddCookieManager();

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                // this lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                // this lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);



            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");

            app.Run();
        }
    }
}