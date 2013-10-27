using System.Web;

namespace Templar.Web.Application
{
    public class VirtualSource : IContentSource
    {
        public string GetContent(HttpContextBase httpContext)
        {
            return "console.log('virtual.js');";
        }
    }
}