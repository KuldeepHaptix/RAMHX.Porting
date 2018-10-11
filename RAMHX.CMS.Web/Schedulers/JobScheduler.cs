using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAMHX.CMS.Web.Schedulers
{
    public class JobScheduler
    {
        public void Execute()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail keepAlive = JobBuilder.Create<SMSProcess>().Build();
            ITrigger triggerKeepAlive = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule(s =>
                     s.WithIntervalInSeconds(30)
                    .OnEveryDay())
                .Build();
            scheduler.ScheduleJob(keepAlive, triggerKeepAlive);
        }
    }
}