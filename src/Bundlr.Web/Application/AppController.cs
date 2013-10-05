using System.Web.Mvc;

namespace Bundlr.Web.Application
{
    public class AppController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}