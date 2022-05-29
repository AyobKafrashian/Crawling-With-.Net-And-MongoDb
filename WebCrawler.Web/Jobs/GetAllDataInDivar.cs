using HtmlAgilityPack;
using Quartz;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClawler.Services.Services.Implementation;
using WebCrawler.DataLayer.Model;

namespace WebCrawler.Web.Jobs
{
    [DisallowConcurrentExecution]
    public class GetAllDataInDivar : IJob
    {
        private readonly DivarServices _divarServices;

        public GetAllDataInDivar(DivarServices divarServices)
        {
            _divarServices = divarServices;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            #region Get All Data And Save To Database

            var url = "https://divar.ir/s/tehran";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var divs = htmlDocument.DocumentNode.Descendants("div").Where(c => c.GetAttributeValue("class", "").Equals("post-card-item kt-col-6 kt-col-xxl-4")).ToList();

            foreach (var item in divs)
            {
                Divar divar = new Divar();

                divar.CreateDate = DateTime.Now;
                divar.Title = item.Descendants("div").FirstOrDefault().InnerText ?? "";
                divar.Price = item.Descendants("div").FirstOrDefault().InnerText ?? "0";
                divar.Address = item.Descendants("span").FirstOrDefault().InnerText ?? "";
                divar.Link = item.Descendants("a").FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value ?? "";

                try
                {
                    divar.ImageUrl = item.Descendants("img").SingleOrDefault().ChildAttributes("data-src").SingleOrDefault().Value;
                }
                catch (NullReferenceException e)
                {
                    divar.ImageUrl = "";
                }

                await _divarServices.CreateAsync(divar);
                #endregion

                await Task.CompletedTask;
            }
        }
    }
}
