using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Templar
{
    public abstract class FileContentSource : IContentSource
    {
        protected abstract Dictionary<string, string> GetSubstitutions(HttpContextBase httpContext);

        public string GetContent(HttpContextBase httpContext)
        {
            string virtualPath = "", absolutePath = "";

            try
            {
                virtualPath = GetVirtualPath();
                absolutePath = httpContext.Server.MapPath(virtualPath);

                string content = File.ReadAllText(absolutePath);

                return GetSubstitutions(httpContext)
                    .Aggregate(content, (script, substitution) => script.Replace(substitution.Key, substitution.Value));
            }
            catch (Exception ex)
            {
                HandleException(ex, absolutePath, virtualPath);
                throw;
            }
        }

        protected abstract string GetVirtualPath();

        protected virtual void HandleException(Exception ex, string path, string filename) { }
    }
}