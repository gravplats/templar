namespace Templar
{
    public class UnderscoreCompiler : Compiler
    {
        public UnderscoreCompiler(string script, string compiler) : base(script, compiler) { }

        public override string Compile(string content)
        {
            return engine.CallGlobalFunction<string>("compile", content);
        }
    }
}