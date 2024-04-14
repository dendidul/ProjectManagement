using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
   

    public static class EntitiesConnection
    {

        public static IServiceCollection RegisterConnections(this IServiceCollection services, IConfiguration configuration)
        {   

            //var x = configuration.GetConnectionString("ProjectManagement");

            services.AddDbContext<ProjectManagementEntities>(options =>               
             options.UseNpgsql(configuration.GetConnectionString("ProjectManagement")));

           

            return services;
        }
    }
}
