using CutURL.BusinessLayer;
using CutURL.Models;
using CutURL.Entities;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace CutURL.Controllers
{
    public class UrlShortnerController : Controller
    {
        // GET: UrlShortner
        private IURLManager _manager;

        public UrlShortnerController(IURLManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            URLModel url = new URLModel();
            return View(url);
        }
        public async Task<ActionResult> Index(URLModel url)
        {
            if (ModelState.IsValid)
            {
                URLDetails shortUrl = await this._manager.ShortenUrl(url.OriginalURL, Request.UserHostAddress);
                url.ShortURL = string.Format("{0}://{1}{2}{3}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"), shortUrl.CustomUrl);
            }
            return View(url);
        }
        public async Task<ActionResult> Click(string segment)
        {
            
            Statistics stat = await this._manager.Click(segment);
            return this.RedirectPermanent(stat.ShortUrl.OrignalUrl);
        }

        public async Task<ActionResult> ShowStats(string segment)
        {
            URLDetails details=null;
            if (ModelState.IsValid)
            {
                details = await _manager.FetchStatistics(segment);
            }
            return View(details);
        }
        public ActionResult GoBack()
        {
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}