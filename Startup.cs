using System;
using LogTransformer.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using LogTransformer.Application.Commands.InsertLogEntry;
using LogTransformer.Application.Models;
using LogTransformer.Application.Commands.TransformLog;
using FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;




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
            services.AddMvcCore()
                     .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                     .AddApiExplorer()
                     .AddJsonOptions(options =>
                     {
                         options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                     });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "LogTransformer API",
                    Version = "v1",
                    Description = "API para transformação de logs"
                });
            });

            services.AddMediatR(typeof(InsertLogEntryCommand));

            services.AddMvcCore().AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<Startup>(); });
            services.AddInfrastructure(Configuration);
            services.AddMvcCore().AddJsonFormatters().AddApiExplorer();

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

            //app.UseStaticFiles(); // Adicione esta linha para servir arquivos estáticos

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LogTransformer API V1");
                c.RoutePrefix = string.Empty; /
            });

            //app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
