using BH.Domain;
using BH.Domain.Interfaces;
using BH.Domain.Repositories;
using BH.Scheduler.Jobs;
using BH.Scheduler.Schedulers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BH.Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            MachineUnlockScheduler.Start(host.Services);
            host.Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => ConfigureServices(services));

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddDbContext<BhDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BH")));
            services.AddTransient<JobFactory>();
            services.AddScoped<IMachinesRepository, MachinesRepository>();
            services.AddScoped<MachineUnlockJob>();
        }
    }
}
