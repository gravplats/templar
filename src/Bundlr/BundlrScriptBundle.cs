using System.Web.Optimization;

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

        public BundlrScriptBundle IncludeSource(string route, IContentSource source)
        {
            Ensure.NotNullOrEmpty(route, "route");
            Ensure.NotNull(source, "source");

            // TODO: handle case where 'route' start with '~/'.
            string virtualPath = "~/" + route;

            virtualPathProvider.AddSource(virtualPath, source);
            return Include(virtualPath);
        }
    }
}