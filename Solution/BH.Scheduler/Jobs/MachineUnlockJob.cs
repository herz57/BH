using BH.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading.Tasks;
 
namespace BH.Scheduler.Jobs
{
    public class MachineUnlockJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public MachineUnlockJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var machinesRepository = _serviceProvider.GetService<IMachinesRepository>();
                await machinesRepository.UnlockMachinesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
