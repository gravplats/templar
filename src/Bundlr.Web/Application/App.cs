using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Bundlr.Web.Application
{
    public class App : HttpApplication
    {
        protected void Application_Start()
        {
            RegisterBundles();
            RegisterFilters();
            RegisterRoutes();
            RegisterViewEngines();
        }

        private static void RegisterBundles()
        {
            var virtualPathProvider = new BundlrVirtualPathProvider(BundleTable.VirtualPathProvider);
            BundleTable.VirtualPathProvider = virtualPathProvider;

            var bundle = new BundlrScriptBundle("~/js", virtualPathProvider)
                .IncludePath("~/Scripts",
                    "hogan.js",
                    "handlebars.runtime.js",
                    "underscore.js",
                    "global.js",
                    "file.js")
                // non-existing files: when optimization is disabled this will map to a custom HTTP handler (BundlrHandler), 
                // when optimization is enabled it will be bundled with the other files.
                // NOTE: please note handlers entry in Web.config which is used so we can 'map' a 'static' files to a routes.
                .IncludeSource("~/Scripts/plain.js", new PlainSource("~/Scripts/plain.txt"))
                .IncludeSource("~/Scripts/virtual.js", new VirtualSource())
                .IncludeHandlebarsTemplates("~/Scripts/handlebars.templates.js", "window._handlebars", "~/Scripts")
                .IncludeMustacheTemplates("~/Scripts/mustache.templates.js", "window._mustache", "~/Scripts")
                .IncludeUnderscoreTemplates("~/Scripts/underscore.templates.js", "window._underscore", "~/Scripts")
                .Include("~/Scripts/templates-tester.js");

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