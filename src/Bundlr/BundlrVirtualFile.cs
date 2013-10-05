using System.IO;
using System.Web.Hosting;

namespace Bundlr
{
    public class BundlrVirtualFile : VirtualFile
    {
        private readonly string content;

        public BundlrVirtualFile(string virtualPath, string content)
            : base(virtualPath)
        {
            this.content = content;
        }

        public override Stream Open()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.Write(content);
            writer.Flush();

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}