using System.Web.Mvc;
using System.Web.Optimization;

namespace Bundlr.Web.Application
{
    public class AppController : Controller
    {
        [HttpGet]
        public ActionResult Index(bool optimize = false)
        {
            BundleTable.EnableOptimizations = optimize;
            return View();
        }

        [HttpGet]
        public ActionResult VirtualJs()
        {
            var content = new VirtualSource().GetContent();
            return Content(content, "text/javascript");
        }
    }
}