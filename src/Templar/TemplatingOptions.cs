using System;
using Templar.Scripts;

namespace Templar
{
    public class TemplatingOptions : ITemplatingOptions
    {
        private Compiler compiler;
        private string searchPattern;

        public ITemplatingOptions WithCustom(Compiler compiler)
        {
            this.compiler = Ensure.NotNull(compiler, "compiler");
            return this;
        }

        public ITemplatingOptions WithHandlebars()
        {
            return WithCustom(new HandlebarsCompiler(Resources.Handlebars, Resources.HandlebarsCompiler));
        }

        public ITemplatingOptions WithHogan()
        {
            return WithCustom(new HoganCompiler(Resources.Hogan, Resources.HoganCompiler));
        }

        public ITemplatingOptions WithUnderscore()
        {
            return WithCustom(new UnderscoreCompiler(Resources.Underscore, Resources.UnderscoreCompiler));
        }

        public ITemplatingOptions WithSearchPattern(string pattern)
        {
            searchPattern = Ensure.NotNullOrEmpty(pattern, "pattern");
            return this;
        }

        public TemplateSource GetTemplateSource(string virtualPath, string global, string templatesVirtualPath)
        {
            Ensure.NotNullOrEmpty(virtualPath, "virtualPath");
            Ensure.NotNullOrEmpty(global, "global");
            Ensure.NotNullOrEmpty(templatesVirtualPath, "templatesVirtualPath");

            if (string.IsNullOrWhiteSpace(searchPattern))
            {
                throw new InvalidOperationException("Missing HTML templates search pattern. Did you forget to specify one in the templating options?");
            }

            if (compiler == null)
            {
                throw new InvalidOperationException("Missing HTML templating language. Did you forget to specify one in the tempating options?");
            }

            return new TemplateSource(global, compiler, new TemplateFinder(templatesVirtualPath, searchPattern));
        }
    }
}