using Core.Functions;
using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleQuartzApplication.Jobs;

namespace SampleQuartzApplication
{
    internal class CronJobInitializer
    {
        public CronJobInitializer() {
            var CronJobInterval = ConfigurationManager.getConfig<Int32>("cron_job:settings:JobIntervalInSeconds");
            Task.Run(() => Init_Job(CronJobInterval));
        }
        public async Task Init_Job(int jobInterval)
        {
            // Quartz Scheduling
            // Grab the Scheduler instance from the Factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<MyJob>()
                .WithIdentity("MyJob", "Group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("MyJobTriggerTrigger", "Group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(jobInterval)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);

        }
    }
}
