using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;

namespace WebCrawler.Web.Jobs
{
    public class SingletonJobFactory : IJobFactory
    {
        #region Constructor
        readonly IServiceProvider _servicesProvider;

        public SingletonJobFactory(IServiceProvider servicesProvider)
        {
            _servicesProvider = servicesProvider;
        }
        #endregion

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _servicesProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
