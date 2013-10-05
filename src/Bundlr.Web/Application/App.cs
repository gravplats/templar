using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Bundlr.Web.Application
{
    public class App : HttpApplication
    {
        private static readonly string VirtualJs = "Scripts/virtual.js";

        protected void Application_Start()
        {
            RegisterBundles();
            RegisterFilters();
            RegisterRoutes();
            RegisterViewEngines();
        }

        private static void RegisterBundles()
        {
            // uncomment to bundle in debug or better yet use '/?optimize=true'.
            //BundleTable.EnableOptimizations = true;

            var virtualPathProvider = new BundlrVirtualPathProvider(BundleTable.VirtualPathProvider);
            BundleTable.VirtualPathProvider = virtualPathProvider;

            var bundle = new BundlrScriptBundle("~/js", virtualPathProvider)
                .Include("~/Scripts/file.js")
                // non-existing file: when optimization is disabled this will map to a controller, when enabled it will be bundled with the other files.
                // NOTE: please note 'VirtualScriptHandler' entry in Web.config which is used so we can 'map' a 'static' file to a route.
                .IncludeSource(VirtualJs, new VirtualSource());

            BundleTable.Bundles.Add(bundle);
        }

        private static void RegisterFilters()
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
        }

        private static void RegisterRoutes()
        {
            var routes = RouteTable.Routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, VirtualJs, new { controller = "App", action = "VirtualJs" });
            routes.MapRoute(null, "{*url}", new { controller = "App", action = "Index" });
        }

        private static void RegisterViewEngines()
        {
            var engines = ViewEngines.Engines;
            engines.Clear();

            var engine = new RazorViewEngine
            {
                AreaViewLocationFormats = new[] { "" },
                AreaMasterLocationFormats = new[] { "" },
                AreaPartialViewLocationFormats = new[] { "" },

                ViewLocationFormats = new[] { "~/Views/{0}.cshtml" },
                MasterLocationFormats = new[] { "" },
                PartialViewLocationFormats = new[] { "~/Views/{0}.cshtml" },

                FileExtensions = new[]
                {
                    "cshtml"
                }
            };

            engines.Add(engine);
        }
    }
}