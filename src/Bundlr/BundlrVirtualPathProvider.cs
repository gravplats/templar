using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Caching;
using System.Web.Hosting;

namespace Bundlr
{
    public class BundlrVirtualPathProvider : VirtualPathProvider
    {
        private readonly Dictionary<string, IContentSource> sources = new Dictionary<string, IContentSource>();

        private readonly VirtualPathProvider provider;

        public BundlrVirtualPathProvider(VirtualPathProvider provider)
        {
            this.provider = provider;
        }

        public void AddSource(string virtualPath, IContentSource source)
        {
            sources.Add(virtualPath, source);
        }

        public override bool DirectoryExists(string virtualDir)
        {
            return provider.DirectoryExists(virtualDir);
        }

        public override bool FileExists(string virtualPath)
        {
            return sources.ContainsKey(virtualPath) || provider.FileExists(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return provider.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            return provider.GetDirectory(virtualDir);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (sources.ContainsKey(virtualPath))
            {
                string content = sources[virtualPath].GetContent();
                return new BundlrVirtualFile(virtualPath, content);
            }

            return provider.GetFile(virtualPath);
        }

        public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            return provider.GetFileHash(virtualPath, virtualPathDependencies);
        }
    }
}