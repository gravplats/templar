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
        public ActionResult PlainJs()
        {
            var content = new PlainSource("~/Scripts/plain.txt").GetContent(HttpContext);
            return Content(content, "text/javascript");
        }

        [HttpGet]
        public ActionResult VirtualJs()
        {
            var content = new VirtualSource().GetContent(HttpContext);
            return Content(content, "text/javascript");
        }
    }
}