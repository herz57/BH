using BH.Scheduler.Jobs;
using Quartz;
using Quartz.Impl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BH.Scheduler.Schedulers
{
    public class MachineUnlockScheduler
    {
        public static async Task Start(IServiceProvider serviceProvider)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
            await scheduler.Start();


            IJobDetail jobDetail = JobBuilder.Create<MachineUnlockJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(opt => 
                {
                    opt.WithIntervalInMinutes(1);
                    opt.RepeatForever();
                }) 
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
