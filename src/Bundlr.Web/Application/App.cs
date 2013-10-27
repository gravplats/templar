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

            var js = new BundlrScriptBundle("~/js", virtualPathProvider)
                .IncludePath("~/assets", "global.js", "file.js")
                // non-existing files: when optimization is disabled these will map to a custom HTTP handler (BundlrHandler), 
                // when optimization is enabled they will be bundled with the other files.
                // NOTE: please note handlers entry in Web.config which is used so we can 'map' a 'static' files to a routes.
                .IncludeSource("~/assets/plain.js", new PlainSource("~/assets/plain.txt"))
                .IncludeSource("~/assets/virtual.js", new VirtualSource());

            var handlebars = new BundlrScriptBundle("~/handlebars", virtualPathProvider)
                .IncludePath("~/assets", "handlebars.runtime.js")
                .IncludeHandlebarsTemplates("~/assets/handlebars.templates.js", "window._templates_handlebars", "~/assets", "*.mustache");

            var hogan = new BundlrScriptBundle("~/hogan", virtualPathProvider)
                .IncludePath("~/assets", "hogan.js")
                .IncludeMustacheTemplates("~/assets/mustache.templates.js", "window._templates_mustache", "~/assets", "*.mustache");

            var underscore = new BundlrScriptBundle("~/underscore", virtualPathProvider)
                .IncludePath("~/assets", "underscore.js")
                .IncludeUnderscoreTemplates("~/assets/underscore.templates.js", "window._templates_underscore", "~/assets", "*._");

            var test = new BundlrScriptBundle("~/test", virtualPathProvider)
                .IncludePath("~/assets", "templates-test.js");

            BundleTable.Bundles.Add(js);
            BundleTable.Bundles.Add(handlebars);
            BundleTable.Bundles.Add(hogan);
            BundleTable.Bundles.Add(underscore);
            BundleTable.Bundles.Add(test);
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