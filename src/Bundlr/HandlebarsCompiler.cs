using Bundlr.Scripts;
using Jurassic;

namespace Bundlr
{
    public class HandlebarsCompiler : Compiler
    {
        private readonly ScriptEngine engine;

        public HandlebarsCompiler()
        {
            engine = new ScriptEngine();
            engine.Execute(_Resources.handlebars);
            engine.Execute(_Resources.handlebars_compiler);
        }

        public override string Compile(string content)
        {
            string template = engine.CallGlobalFunction<string>("compile", content);
            return string.Format("Handlebars.template({0});", template);
        }
    }
}