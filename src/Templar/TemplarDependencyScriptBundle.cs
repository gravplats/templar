namespace Templar
{
    public class TemplarDependencyScriptBundle : TemplarScriptBundle
    {
        private readonly DependencyOrderer orderer;

        public TemplarDependencyScriptBundle(string virtualPath, TemplarVirtualPathProvider virtualPathProvider)
            : base(virtualPath, virtualPathProvider)
        {
            Orderer = orderer = new DependencyOrderer();
        }

        public override TemplarScriptBundle Include(params string[] virtualPaths)
        {
            AddExplicitOrder(virtualPaths);
            return base.Include(virtualPaths);
        }

        public override TemplarScriptBundle IncludeSource(string virtualPath, IContentSource source)
        {
            AddExplicitOrder(virtualPath);
            return base.IncludeSource(virtualPath, source);
        }

        private void AddExplicitOrder(params string[] virtualPaths)
        {
            foreach (var virtualPath in virtualPaths)
            {
                // remove leading '~'.
                string path = virtualPath.Substring(1);
                orderer.ExplicitOrder.Add(path);
            }
        }
    }
}