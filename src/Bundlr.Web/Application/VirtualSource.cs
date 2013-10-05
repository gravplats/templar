namespace Bundlr.Web.Application
{
    public class VirtualSource : IContentSource
    {
        public string GetContent()
        {
            return "console.log('virtual.js');";
        }
    }
}