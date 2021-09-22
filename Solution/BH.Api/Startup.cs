using BH.Infrastructure.Interfaces;
using Domain;
using BH.Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BH.Domain.Seed;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

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
                options.Cookie.Name = "AuthCookie";
                options.ExpireTimeSpan = TimeSpan.FromHours(24);
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

            services.AddTransient<ITicketsService, TicketsService>();
            services.AddTransient<ITicketsRepository, TicketsRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseRouting();
            app.UseCors(MyOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
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
