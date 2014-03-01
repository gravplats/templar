using System;
using System.IO;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Templar.Scripts;

namespace Templar
{
    public class TemplarScriptBundle : ScriptBundle
    {
        private readonly TemplarVirtualPathProvider virtualPathProvider;

        public TemplarScriptBundle(string virtualPath, TemplarVirtualPathProvider virtualPathProvider)
            : base(virtualPath)
        {
            this.virtualPathProvider = Ensure.NotNull(virtualPathProvider, "virtualPathProvider");
        }

        public virtual new TemplarScriptBundle Include(params string[] virtualPaths)
        {
            return base.Include(virtualPaths) as TemplarScriptBundle;
        }

        public new TemplarScriptBundle IncludeDirectory(string directoryVirtualPath, string searchPattern, bool searchSubdirectories = true)
        {
            return base.IncludeDirectory(directoryVirtualPath, searchPattern, searchSubdirectories) as TemplarScriptBundle;
        }

        public TemplarScriptBundle IncludePath(string root, params string[] files)
        {
            if (!root.EndsWith("/"))
            {
                root = root + "/";
            }

            foreach (string file in files)
            {
                string path = VirtualPathUtility.Combine(root, file);
                Include(path);
            }

            return this;
        }

        public virtual TemplarScriptBundle IncludeSource(string virtualPath, IContentSource source)
        {
            Ensure.NotNullOrEmpty(virtualPath, "virtualPaths");
            Ensure.NotNull(source, "source");

            // don't expose the source when bundling.
            if (!BundleTable.EnableOptimizations)
            {
                // assume that the virtual path starts with '~/'.
                string url = virtualPath.Substring(2);

                var route = new Route(url, new TemplarHandler(source));
                RouteTable.Routes.Add(route);
            }

            virtualPathProvider.AddSource(virtualPath, source);
            return Include(virtualPath);
        }

        public TemplarScriptBundle IncludeHtmlTemplates(string virtualPath, string global, string templatesVirtualPath, Action<ITemplatingOptions> customize)
        {
            var options = new TemplatingOptions();
            customize(options);

            var source = options.GetTemplateSource(virtualPath, global, templatesVirtualPath);
            return IncludeSource(virtualPath, source);
        }

        public TemplarScriptBundle IncludeHandlebarsTemplates(string virtualPath, string global, string templatesVirtualPath, string searchPattern = "*.mustache")
        {
            return IncludeHtmlTemplates(virtualPath, global, templatesVirtualPath, opt => opt.WithHandlebars().WithSearchPattern(searchPattern));
        }

        public TemplarScriptBundle IncludeMustacheTemplates(string virtualPath, string global, string templatesVirtualPath, string searchPattern = "*.mustache")
        {
            return IncludeHtmlTemplates(virtualPath, global, templatesVirtualPath, opt => opt.WithHogan().WithSearchPattern(searchPattern));
        }

        public TemplarScriptBundle IncludeUnderscoreTemplates(string virtualPath, string global, string templatesVirtualPath, string searchPattern = "*._")
        {
            return IncludeHtmlTemplates(virtualPath, global, templatesVirtualPath, opt => opt.WithUnderscore().WithSearchPattern(searchPattern));
        }
    }
}