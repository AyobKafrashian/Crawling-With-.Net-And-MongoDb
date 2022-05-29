using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawler.Web.Jobs.Time
{
    public class JobSchedule
    {
        public Type JobType { get; set; }
        //زمان اجرا
        public string CronExpression { get; set; }

        #region Constructor
        public JobSchedule(Type jobType, string cronExpression)
        {
            JobType = jobType;
            CronExpression = cronExpression;
        }
        #endregion
    }
}
