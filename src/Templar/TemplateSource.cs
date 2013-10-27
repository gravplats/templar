using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Templar
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

                var results = Compile(templates);
                foreach (var result in results)
                {
                    writer.WriteLine(result);
                }

                writer.WriteLine("}();");

                return writer.ToString();
            }
        }

        private IEnumerable<string> Compile(IEnumerable<Template> templates)
        {
            return templates.AsParallel().Select(template =>
            {
                string name = template.GetName();
                string content = compiler.Compile(template.GetContent());

                return string.Format("  templates['{0}'] = {1};", name, content);
            });
        }
    }
}