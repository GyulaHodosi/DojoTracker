using System;
using DojoTracker.Models;
using DojoTracker.Services.AccountManagement;
using DojoTracker.Services.AccountManagement.Interfaces;
using DojoTracker.Services.Repositories;
using DojoTracker.Services.Repositories.Interfaces;
using DojoTracker.Services.Statistics;
using DojoTracker.Services.Statistics.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DojoTracker
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
            
            services.AddScoped(typeof(ISolutionRepository), typeof(SolutionRepository));
            services.AddScoped(typeof(IDojoRepository), typeof(DojoRepository));
            services.AddScoped(typeof(IEmailService), typeof(EmailService));
            services.AddScoped(typeof(IStatGenerator), typeof(StatGenerator));
            services.AddScoped(typeof(IAccountManager), typeof(AccountManager));
            services.AddControllers();
            services.AddDbContextPool<DojoTrackerDbContext>(option => { option.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")); });
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DojoTrackerDbContext>();
            services.ConfigureApplicationCookie(options=>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "credentials";
                options.ExpireTimeSpan = TimeSpan.FromHours(24);
                /*options.Cookie.Domain = "localhost"; */
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
            
            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "ClientApp/build";
            });
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            
            app.UseCors(options => options.WithOrigins("https://dojotracker.herokuapp.com", "http://dojotracker.herokuapp.com").AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
