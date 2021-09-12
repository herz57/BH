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

namespace BH.Api
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
            services.AddControllers();
            services.AddDbContext<BhDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BH")));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BhDbContext>();

            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", opt =>
            //    {
            //        opt.RequireHttpsMetadata = false;
            //        opt.Authority = "https://localhost:5005";
            //        opt.Audience = "companyApi";
            //    });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
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
            app.UseCors();

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
