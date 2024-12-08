using LogTransformer.Application.Services;
using LogTransformer.Core.Repositories;
using LogTransformer.Infrastructure.Persistence;
using LogTransformer.Infrastructure.Persistence.Repositories;
using LogTransformer.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace LogTransformer.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
              .AddData(configuration)
              .AddRepositories(); 
            return services;
        }

        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LogTransformerCs");

            services.AddDbContext<LogDbContext>(o => o.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILogEntryRepository, LogEntryRepository>();
            services.AddScoped<ITransformedLogRepository, TransformedLogRepository>();
            services.AddHttpClient<IFileService, FileService>();
            return services;
        }
    }
}
