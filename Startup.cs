using System;
using LogTransformer.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configura a versão da MVC para compatibilidade com o .NET Core 2.1
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Configuração do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "LogTransformer API",
                    Version = "v1",
                    Description = "API para transformação de logs"
                });
            });

            // Adiciona a infraestrutura (se você tiver implementações adicionais, como DbContext ou Repositories)
            services.AddInfrastructure(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            // Usar o middleware Swagger
            app.UseSwagger();

            // Configuração da UI do Swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LogTransformer API V1");
                c.RoutePrefix = "swagger";  // Serve a UI do Swagger na raiz (opcional)
            });

            // Adiciona suporte para requisições HTTPS (recomendado para produção)
            app.UseHttpsRedirection();

            // Roteamento MVC
            app.UseMvc();
        }
    }
}
