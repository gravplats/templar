namespace Templar
{
    public class HandlebarsCompiler : Compiler
    {
        public HandlebarsCompiler(string script, string compiler) : base(script, compiler) { }

        public override string Compile(string content)
        {
            string template = engine.CallGlobalFunction<string>("compile", content);
            return string.Format("Handlebars.template({0});", template);
        }
    }
}