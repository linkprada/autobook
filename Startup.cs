using System;
using autobook.Controllers;
using autobook.Core;
using autobook.Core.Models;
using autobook.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace autobook
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
            // solving issue with .NET CORE 3.X the serialize/deserialize done using System.Text.Json 
            // which doesn’t deserialize non-string values like Int, Boolean and other primitives into string properties. 
            // Any non-string value conversion produce JsonException
            services.AddControllers().AddNewtonsoftJson();

            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));

            services.AddScoped<IPhotoRepository,PhotoRepository>();
            services.AddScoped<IVehiculeRepository,VehiculeRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddTransient<IPhotoService,PhotoService>();
            services.AddTransient<IPhotoStorage,FileSystemPhotoStorage>();


            services.AddAutoMapper(typeof(Startup));

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AutoBookDbConnection"));
            });

            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // }).AddJwtBearer(options =>
            // {
            //     options.Authority = "https://autobook.eu.auth0.com/";
            //     options.Audience = "https://api.autobook.com";
            // });

            // services.AddAuthorization(options => options.AddPolicy(AppPolicies.RequiredAdminRole,policy => policy.RequireClaim("https://autobook.com/roles","Admin")));


            services.AddMvc(options => options.EnableEndpointRouting = false);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
