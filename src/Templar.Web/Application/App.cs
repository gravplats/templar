using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Templar.Web.Application
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
            var virtualPathProvider = new TemplarVirtualPathProvider(BundleTable.VirtualPathProvider);
            BundleTable.VirtualPathProvider = virtualPathProvider;

            var js = new TemplarScriptBundle("~/js", virtualPathProvider)
                .IncludePath("~/assets", "global.js", "file.js")
                // non-existing files: when optimization is disabled these will map to a custom HTTP handler (TemplarHandler), 
                // when optimization is enabled they will be bundled with the other files.
                // NOTE: please note handlers entry in Web.config which is used so we can 'map' a 'static' files to a routes.
                .IncludeSource("~/assets/plain.js", new PlainSource("~/assets/plain.txt"))
                .IncludeSource("~/assets/virtual.js", new VirtualSource());

            var handlebars = new TemplarScriptBundle("~/handlebars", virtualPathProvider)
                .IncludePath("~/assets", "handlebars.runtime.js")
                .IncludeHandlebarsTemplates("~/assets/handlebars.templates.js", "window._templates_handlebars", "~/assets", "*.mustache");

            var hogan = new TemplarScriptBundle("~/hogan", virtualPathProvider)
                .IncludePath("~/assets", "hogan.js")
                .IncludeMustacheTemplates("~/assets/mustache.templates.js", "window._templates_mustache", "~/assets", "*.mustache");

            var underscore = new TemplarScriptBundle("~/underscore", virtualPathProvider)
                .IncludePath("~/assets", "underscore.js")
                .IncludeUnderscoreTemplates("~/assets/underscore.templates.js", "window._templates_underscore", "~/assets", "*._");

            var test = new TemplarScriptBundle("~/test", virtualPathProvider)
                .IncludePath("~/assets", "templates-test.js");

            BundleTable.Bundles.Add(js);
            BundleTable.Bundles.Add(handlebars);
            BundleTable.Bundles.Add(hogan);
            BundleTable.Bundles.Add(underscore);
            BundleTable.Bundles.Add(test);

            var dependencies = new TemplarDependencyScriptBundle("~/dep", virtualPathProvider)
                .Include("~/assets/dependencies/e.js")
                .IncludeDirectory("~/assets/dependencies", "*.js");

            BundleTable.Bundles.Add(dependencies);
        }

        private static void RegisterFilters()
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
        }

        private static void RegisterRoutes()
        {
            var routes = RouteTable.Routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "dependencies", new { controller = "App", action = "Dependencies" });
            routes.MapRoute(null, "", new { controller = "App", action = "Index" });
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