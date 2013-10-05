using Bundlr.Scripts;
using Jurassic;

namespace Bundlr
{
    public class UnderscoreCompiler : Compiler
    {
        private readonly ScriptEngine engine;

        public UnderscoreCompiler()
        {
            engine = new ScriptEngine();
            engine.Execute(_Resources.underscore);
            engine.Execute(_Resources.underscore_compiler);
        }

        public override string Compile(string content)
        {
            return engine.CallGlobalFunction<string>("compile", content);
        }
    }
}