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
            string filename = "", path = "";

            try
            {
                string root = GetRootPath().TrimEnd('/');

                filename = GetFilename();
                path = httpContext.Server.MapPath(root + "/" + filename);

                string content = File.ReadAllText(path);

                return GetSubstitutions(httpContext)
                    .Aggregate(content, (script, substitution) => script.Replace(substitution.Key, substitution.Value));
            }
            catch (Exception ex)
            {
                HandleException(ex, path, filename);
                throw;
            }
        }

        protected abstract string GetRootPath();
        protected abstract string GetFilename();

        protected virtual void HandleException(Exception ex, string path, string filename) { }
    }
}