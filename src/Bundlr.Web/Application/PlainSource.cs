using System.IO;
using System.Web;

namespace Bundlr.Web.Application
{
    public class PlainSource : IContentSource
    {
        private readonly string virtualPath;

        public PlainSource(string virtualPath)
        {
            this.virtualPath = virtualPath;
        }

        public string GetContent(HttpContextBase httpContext)
        {
            string physicalPath = httpContext.Server.MapPath(virtualPath);
            return File.ReadAllText(physicalPath);
        }
    }
}