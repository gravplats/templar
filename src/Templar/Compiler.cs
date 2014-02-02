using Jurassic;

namespace Templar
{
    public abstract class Compiler
    {
        protected readonly ScriptEngine engine;

        protected Compiler(string script, string compiler)
        {
            engine = new ScriptEngine();
            engine.Execute(Ensure.NotNullOrEmpty(script, "script"));
            engine.Execute(Ensure.NotNullOrEmpty(compiler, "compiler"));
        }

        public abstract string Compile(string content);
    }
}