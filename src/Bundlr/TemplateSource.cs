using System.IO;
using System.Web;

namespace Bundlr
{
    public class TemplateSource : IContentSource
    {
        private readonly string global;
        private readonly Compiler compiler;
        private readonly TemplateFinder finder;

        public TemplateSource(string global, Compiler compiler, TemplateFinder finder)
        {
            this.global = global;
            this.compiler = compiler;
            this.finder = finder;
        }

        public string GetContent(HttpContextBase httpContext)
        {
            var templates = finder.Find(httpContext);
            using (var writer = new StringWriter())
            {
                writer.WriteLine("!function() {");
                writer.WriteLine("  var templates = {0}.templates = {{}};", global);

                foreach (var template in templates)
                {
                    string name = template.GetName();
                    string content = compiler.Compile(template.GetContent());

                    writer.WriteLine("  templates['{0}'] = {1};", name, content);
                }

                writer.WriteLine("}();");

                return writer.ToString();
            }
        }
    }
}