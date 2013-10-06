using System.Web.Optimization;
using System.Web.Routing;

namespace Bundlr
{
    public class BundlrScriptBundle : ScriptBundle
    {
        private readonly BundlrVirtualPathProvider virtualPathProvider;

        public BundlrScriptBundle(string virtualPath, BundlrVirtualPathProvider virtualPathProvider)
            : base(virtualPath)
        {
            this.virtualPathProvider = Ensure.NotNull(virtualPathProvider, "virtualPathProvider");
        }

        public new BundlrScriptBundle Include(params string[] virtualPath)
        {
            return base.Include(virtualPath) as BundlrScriptBundle;
        }

        public new BundlrScriptBundle IncludeDirectory(string directoryVirtualPath, string searchPattern, bool searchSubdirectories = false)
        {
            return base.IncludeDirectory(directoryVirtualPath, searchPattern, searchSubdirectories) as BundlrScriptBundle;
        }

        public BundlrScriptBundle IncludeSource(string virtualPath, IContentSource source)
        {
            Ensure.NotNullOrEmpty(virtualPath, "virtualPath");
            Ensure.NotNull(source, "source");

            // don't expose the source when bundling.
            if (!BundleTable.EnableOptimizations)
            {
                // assume that the virtual path starts with '~/'.
                string url = virtualPath.Substring(2);

                var route = new Route(url, new BundlrHandler(source));
                RouteTable.Routes.Add(route);
            }

            virtualPathProvider.AddSource(virtualPath, source);
            return Include(virtualPath);
        }

        public BundlrScriptBundle IncludeHandlebarsTemplates(string virtualPath, string global, string templatesVirtualPath, string searchPattern = "*.mustache")
        {
            var compiler = new HandlebarsCompiler();
            var source = new TemplateSource(global, compiler, new TemplateFinder(templatesVirtualPath, searchPattern));

            return IncludeSource(virtualPath, source);
        }

        public BundlrScriptBundle IncludeMustacheTemplates(string virtualPath, string global, string templatesVirtualPath, string searchPattern = "*.mustache")
        {
            var compiler = new HoganCompiler();
            var source = new TemplateSource(global, compiler, new TemplateFinder(templatesVirtualPath, searchPattern));

            return IncludeSource(virtualPath, source);
        }

        public BundlrScriptBundle IncludeUnderscoreTemplates(string virtualPath, string global, string templatesVirtualPath, string searchPattern = "*._")
        {
            var compiler = new UnderscoreCompiler();
            var source = new TemplateSource(global, compiler, new TemplateFinder(templatesVirtualPath, searchPattern));

            return IncludeSource(virtualPath, source);
        }
    }
}