using Quartz;
using Quartz.Impl;
using RAMHX.CMS.Web.Areas.Admin.Schedulers;
using RAMHX.CMS.Web.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAMHX.CMS.Web.Areas.Admin.Scheduler
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            /*Keep Alive Application*/
            IJobDetail keepAlive = JobBuilder.Create<KeepAlive>().Build();
            ITrigger triggerKeepAlive = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule(s =>
                     s.WithIntervalInMinutes(20)
                    .OnEveryDay())
                .Build();
            scheduler.ScheduleJob(keepAlive, triggerKeepAlive);

            /*Package Installation Scheduler*/
            IJobDetail instlPackage = JobBuilder.Create<InstallPackageScheduler>().Build();
            ITrigger triginstlPackage = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule(s =>
                     s.WithIntervalInSeconds(5)
                    .OnEveryDay())
                .Build();
            scheduler.ScheduleJob(instlPackage, triginstlPackage);

            IJobDetail sms = JobBuilder.Create<SMSProcess>().Build();
            ITrigger triggersms = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule(s =>
                     s.WithIntervalInSeconds(30)
                    .OnEveryDay())
                .Build();
            scheduler.ScheduleJob(sms, triggersms);
        }
    }
}