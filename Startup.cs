using System;
using LogTransformer.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using LogTransformer.Application.Commands.InsertLogEntry;

namespace LogTransformer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "LogTransformer API",
                    Version = "v1",
                    Description = "API para transformação de logs"
                });
            });
            services.AddMediatR(typeof(InsertLogEntryCommand));

            services.AddInfrastructure(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LogTransformer API V1");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
