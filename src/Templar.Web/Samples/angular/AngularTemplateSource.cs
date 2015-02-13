using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Templar.Web.Samples.angular
{
    public class AngularTemplateSource : FileContentSource
    {
        private readonly TemplateFinder finder;

        public AngularTemplateSource(string virtualPath, string searchPattern)
        {
            finder = new TemplateFinder(virtualPath, searchPattern);
        }

        protected override Dictionary<string, string> GetSubstitutions(HttpContextBase httpContext)
        {
            using (var writer = new StringWriter())
            {
                var templates = finder.Find(httpContext);
                foreach (var template in templates)
                {
                    string name = template.GetName() + ".html";
                    string content = HttpUtility.JavaScriptStringEncode(template.GetContent());

                    writer.WriteLine("        $templateCache.put('{0}', '{1}');", name, content);
                }

                return new Dictionary<string, string>
                {
                    { "'__templates__';", writer.ToString() }
                };
            }
        }

        protected override string GetVirtualPath()
        {
            return "~/Samples/angular/templates.js";
        }
    }
}