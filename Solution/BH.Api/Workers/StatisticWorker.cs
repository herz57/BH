using BH.Common.Dtos;
using BH.Infrastructure.Hubs;
using BH.Infrastructure.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BH.Api.Workers
{
    public class StatisticWorker : BackgroundService
    {
        private readonly IHubContext<StatisticHub> _statisticHub;
        private readonly IServiceScopeFactory _scopeFactory;

        public StatisticWorker(IHubContext<StatisticHub> statisticHub,
            IServiceScopeFactory scopeFactory)
        {
            _statisticHub = statisticHub;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var profilesService = scope.ServiceProvider.GetRequiredService<IProfilesService>();
                var machinesService = scope.ServiceProvider.GetRequiredService<IMachinesService>();
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = new StatisticDto
                    {
                        MachinesState = machinesService.GetMachinesState(),
                        UserStatistics = profilesService.GetUsersStatistics(),
                    };

                    var serialized = JsonConvert.SerializeObject(result);
                    await _statisticHub.Clients.All.SendAsync("statistic", serialized);
                    await Task.Delay(500);
                }
            }
        }
    }
}