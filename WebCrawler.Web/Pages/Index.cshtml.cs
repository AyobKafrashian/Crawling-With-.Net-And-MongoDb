using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebClawler.Services.Services.Implementation;
using WebCrawler.DataLayer.Model;

namespace WebCrawler.Web.Pages
{
    public class IndexModel : PageModel
    {
        #region Constructor
        private readonly DivarServices _divarServies;

        public IndexModel(DivarServices divarServies)
        {
            _divarServies = divarServies;
        }
        #endregion

        public List<Divar> Divar { get; set; }

        public async Task OnGet()
        {
            Divar = await _divarServies.GetAsync();
        }
    }
}
