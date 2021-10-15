using Quartz;
using Quartz.Spi;
using System;

namespace BH.Scheduler
{
    public class JobFactory : IJobFactory
    {
        protected readonly IServiceProvider _serviceProvider;

        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var job = _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
            return job;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}