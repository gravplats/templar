using System.Web;

namespace Templar
{
    public interface IContentSource
    {
        string GetContent(HttpContextBase httpContext);
    }
}