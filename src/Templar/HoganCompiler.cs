using Jurassic;
using Templar.Scripts;

namespace Templar
{
    public class HoganCompiler : Compiler
    {
        private readonly ScriptEngine engine;

        public HoganCompiler()
        {
            engine = new ScriptEngine();
            engine.Execute(Resources.Hogan);
            engine.Execute(Resources.HoganCompiler);
        }

        public override string Compile(string content)
        {
            string template = engine.CallGlobalFunction<string>("compile", content);
            return string.Format("new Hogan.Template({0});", template);
        }
    }
}