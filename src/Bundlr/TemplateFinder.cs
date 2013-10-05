using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Bundlr
{
    public class TemplateFinder
    {
        private readonly string virtualPath;
        private readonly string searchPattern;

        public TemplateFinder(string virtualPath, string searchPattern)
        {
            this.virtualPath = Ensure.NotNullOrEmpty(virtualPath, "virtualPath");
            this.searchPattern = Ensure.NotNullOrEmpty(searchPattern, "searchPattern");
        }

        public virtual IEnumerable<Template> Find(HttpContextBase httpContext)
        {
            string physicalPath = httpContext.Server.MapPath(virtualPath);
            var directory = new DirectoryInfo(physicalPath);

            string physicalApplicationPath = httpContext.Request.PhysicalApplicationPath;
            if (string.IsNullOrEmpty(physicalApplicationPath))
            {
                throw new InvalidOperationException("Unknown physical application path.");
            }

            return directory.GetFiles(searchPattern, SearchOption.AllDirectories)
                            .Select(fi => new Template(fi));
        }
    }
}