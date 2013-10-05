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

        public BundlrScriptBundle IncludeSource(string url, IContentSource source)
        {
            Ensure.NotNullOrEmpty(url, "url");
            Ensure.NotNull(source, "source");

            // TODO: handle case where 'url' start with '~/'.
            string virtualPath = "~/" + url;

            var route = new Route(url, new BundlrHandler(source));
            RouteTable.Routes.Add(route);

            virtualPathProvider.AddSource(virtualPath, source);
            return Include(virtualPath);
        }

        public BundlrScriptBundle IncludeMustacheTemplates(string url, string global, string virtualPath, string searchPattern = "*.mustache")
        {
            var compiler = new HoganCompiler();
            var source = new TemplateSource(global, compiler, new TemplateFinder(virtualPath, searchPattern));

            return IncludeSource(url, source);
        }

        public BundlrScriptBundle IncludeUnderscoreTemplates(string url, string global, string virtualPath, string searchPattern = "*._")
        {
            var compiler = new UnderscoreCompiler();
            var source = new TemplateSource(global, compiler, new TemplateFinder(virtualPath, searchPattern));

            return IncludeSource(url, source);
        }
    }
}