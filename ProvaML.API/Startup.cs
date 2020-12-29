using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProvaML.Application;
using ProvaML.Infrastructure.Data;
using ProvaML.Domain.Repositories;
using ProvaML.Infrastructure;
using ProvaML.Infrastructure.File;

namespace ProvaML.API
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Prova ML API", 
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Leandro Ribeiro",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/leandroribeiro")
                    }
                });
            });

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<ProvaMLContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddSingleton<IImageStorage, LocalImageStorage>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //TODO
            //app.UseHttpsRedirection();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            //TODO
            //app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProvaML API v1"));
        }
    }
}