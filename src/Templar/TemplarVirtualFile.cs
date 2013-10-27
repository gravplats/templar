using System.IO;
using System.Web;
using System.Web.Hosting;

namespace Templar
{
    public class TemplarVirtualFile : VirtualFile
    {
        private readonly IContentSource source;

        public TemplarVirtualFile(string virtualPath, IContentSource source)
            : base(virtualPath)
        {
            this.source = source;
        }

        public override Stream Open()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            var context = new HttpContextWrapper(HttpContext.Current);
            string content = source.GetContent(context);

            writer.Write(content);
            writer.Flush();

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}