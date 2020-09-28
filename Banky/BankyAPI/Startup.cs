using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using BankyAPI.BankyMapper;
using BankyAPI.Data;
using BankyAPI.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BankyAPI
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
            //----------------Add DBCOntext Dependency Injection here.
            services.AddDbContext<BankyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //----------------Add Repository Interface and implemeter Class for DI here.
            services.AddScoped<IBankRepository, BankRepository>();

            //----------------Add Mapping class for DTO for DI here.
            services.AddAutoMapper(typeof(BankyMappings));

            //----------------Add SwashBuckle for Swagger.
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("BankyOpenAPISpec",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Banky API",
                        Version = "1",
                        Description = "Banky API made by Anika Nahar",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "anikanahar.cse@gmail.com",
                            Name = "Anika Nahar",
                           // Url = new Uri("")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                        }
                    });

            //----------------Add XML Comment to the Swagger UI.
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentsFullPath);

            });

            //-----------------APIs Don't have ControllerswithViews like MVC
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
                { 
                    options.SwaggerEndpoint("/swagger/BankyOpenAPISpec/swagger.json", "Banky API");
                    options.RoutePrefix = ""; //Starts SwaggerUI at launch
                });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
