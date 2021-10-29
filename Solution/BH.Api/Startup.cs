using BH.Infrastructure.Interfaces;
using BH.Domain;
using BH.Domain.Entities;
using BH.Domain.Interfaces;
using BH.Domain.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BH.Domain.Seed;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using BH.Infrastructure.Services;
using BH.Api.Workers;
using BH.Infrastructure.Hubs;

namespace BH.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyOrigins = "MyOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddDbContext<BhDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BH")));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BhDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                var cookieSettings = Configuration.GetSection("Cookies");
                options.Cookie.Name = cookieSettings.GetValue<string>("Name");
                options.ExpireTimeSpan = TimeSpan.FromHours(cookieSettings.GetValue<int>("ExpireTimeSpan"));
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyOrigins,
                    builder => {
                        builder.WithOrigins("https://localhost:5001")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

            services.AddSignalR();
            services.AddHostedService<StatisticWorker>();

            services.AddTransient<ITicketsService, TicketsService>();
            services.AddTransient<IMachinesService, MachinesService>();
            services.AddTransient<IProfilesService, ProfilesService>();
            services.AddTransient<ILoggerService, LoggerService>();

            services.AddTransient<ITicketsRepository, TicketsRepository>();
            services.AddTransient<IProfilesRepository, ProfilesRepository>();
            services.AddTransient<IMachinesRepository, MachinesRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors(MyOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<StatisticHub>("statistic-hub");
                endpoints.MapControllers();
            });

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var identitySeeder = new IdentitySeeder(scope);
                identitySeeder.SeedAsync().Wait();
            }
        }
    }
}
