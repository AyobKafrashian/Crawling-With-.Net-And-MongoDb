using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using WebClawler.Services.Services.Implementation;
using WebCrawler.Web.Jobs;
using WebCrawler.Web.Jobs.Start_And_Stop;
using WebCrawler.Web.Jobs.Time;
using WebCrawler.Web.Utilities;

namespace WebCrawler.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region IOC
            services.AddSingleton<DivarServices>();
            #endregion

            #region Quartz
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<GetAllDataInDivar>();
            services.AddSingleton(new JobSchedule(jobType: typeof(GetAllDataInDivar), cronExpression: "0 0 12 * * ?"));
            services.AddHostedService<QuartzHostedService>();
            #endregion

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
