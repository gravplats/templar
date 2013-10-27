using System.IO;

namespace Templar
{
    public class Template
    {
        private readonly FileInfo fi;

        public Template(FileInfo fi)
        {
            this.fi = Ensure.NotNull(fi, "fi");
        }

        public virtual string GetName()
        {
            return Path.GetFileNameWithoutExtension(fi.Name);
        }

        public virtual string GetContent()
        {
            return File.ReadAllText(fi.FullName);
        }
    }
}