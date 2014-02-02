namespace Templar
{
    public class HoganCompiler : Compiler
    {
        public HoganCompiler(string script, string compiler) : base(script, compiler) { }

        public override string Compile(string content)
        {
            string template = engine.CallGlobalFunction<string>("compile", content);
            return string.Format("new Hogan.Template({0});", template);
        }
    }
}