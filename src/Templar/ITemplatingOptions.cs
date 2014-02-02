namespace Templar
{
    public interface ITemplatingOptions
    {
        ITemplatingOptions WithCustom(Compiler compiler);
        ITemplatingOptions WithHandlebars();
        ITemplatingOptions WithHogan();
        ITemplatingOptions WithUnderscore();
        ITemplatingOptions WithSearchPattern(string pattern);
    }
}