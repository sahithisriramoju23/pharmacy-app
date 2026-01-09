using Medicine.Application.Data;
using Medicine.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services here
            services.AddScoped<ICosmosDbContext, CosmosDbContext>();
            services.AddDbContextFactory<CosmosDbContext>(optionsBuilder =>
                  optionsBuilder
                    .UseCosmos(
                      connectionString: configuration.GetConnectionString("DefaultConnection")!,
                      databaseName: "MedicineDB",
                      cosmosOptionsAction: options =>
                      {
                          options.ConnectionMode(Microsoft.Azure.Cosmos.ConnectionMode.Direct);
                          options.MaxRequestsPerTcpConnection(16);
                          options.MaxTcpConnectionsPerEndpoint(32);
                      }));
            return services;
        }
    }
}
