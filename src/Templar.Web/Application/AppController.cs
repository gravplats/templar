using System.Web.Mvc;
using System.Web.Optimization;

namespace Templar.Web.Application
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
        public ActionResult Dependencies()
        {
            return View();
        }
    }
}