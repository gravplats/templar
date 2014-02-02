using Jurassic;
using Templar.Scripts;

namespace Templar
{
    public class UnderscoreCompiler : Compiler
    {
        private readonly ScriptEngine engine;

        public UnderscoreCompiler()
        {
            engine = new ScriptEngine();
            engine.Execute(Resources.Underscore);
            engine.Execute(Resources.UnderscoreCompiler);
        }

        public override string Compile(string content)
        {
            return engine.CallGlobalFunction<string>("compile", content);
        }
    }
}