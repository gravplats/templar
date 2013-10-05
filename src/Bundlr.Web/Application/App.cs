using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bundlr.Web.Application
{
    public class App : HttpApplication
    {
        protected void Application_Start()
        {
            RegisterFilters();
            RegisterRoutes();
            RegisterViewEngines();
        }

        private static void RegisterFilters()
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
        }

        private static void RegisterRoutes()
        {
            var routes = RouteTable.Routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{*url}",
                defaults: new { controller = "App", action = "Index" }
            );
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