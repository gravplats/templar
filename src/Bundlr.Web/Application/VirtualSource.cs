using System.Web;

namespace Bundlr.Web.Application
{
    public class VirtualSource : IContentSource
    {
        public string GetContent(HttpContextBase httpContext)
        {
            return "console.log('virtual.js');";
        }
    }
}