using System.Web;

namespace Bundlr
{
    public interface IContentSource
    {
        string GetContent(HttpContextBase httpContext);
    }
}