using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Optimization;

namespace Templar
{
    public class DependencyOrderer : IBundleOrderer
    {
        // http://stackoverflow.com/questions/4106862/how-to-sort-depended-objects-by-dependency
        class TopologicalSort<T>
        {
            private readonly IEnumerable<T> source;
            private readonly Func<T, IEnumerable<T>> dependencyProvider;

            public TopologicalSort(IEnumerable<T> source, Func<T, IEnumerable<T>> dependencyProvider)
            {
                this.source = Ensure.NotNull(source, "source");
                this.dependencyProvider = Ensure.NotNull(dependencyProvider, "dependencyProvider");
            }

            public IEnumerable<T> Sort()
            {
                var sorted = new List<T>();
                var visited = new HashSet<T>();

                foreach (var item in source)
                {
                    Visit(item, visited, sorted);
                }

                return sorted;
            }

            private void Visit(T item, ISet<T> visited, ICollection<T> sorted)
            {
                if (visited.Contains(item))
                {
                    return;
                }

                visited.Add(item);

                foreach (var dependency in dependencyProvider(item))
                {
                    Visit(dependency, visited, sorted);
                }

                sorted.Add(item);
            }
        }

        public static Regex DefaultRequireRegex = new Regex(@"// require\('(?<path>.*)'\);", RegexOptions.Compiled);

        private readonly Regex regex;

        public DependencyOrderer(Regex regex = null)
        {
            this.regex = regex ?? DefaultRequireRegex;
            ExplicitOrder = new HashSet<string>();
        }

        public ISet<string> ExplicitOrder { get; private set; }

        public class BundleFileEqualityComparer : IEqualityComparer<BundleFile>
        {
            public bool Equals(BundleFile file1, BundleFile file2)
            {
                return file1.VirtualFile.VirtualPath == file2.VirtualFile.VirtualPath;
            }

            public int GetHashCode(BundleFile file)
            {
                return file.VirtualFile.VirtualPath.GetHashCode();
            }
        }

        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            var comparer = new BundleFileEqualityComparer();

            var explicitlyAdded = new HashSet<BundleFile>(comparer);
            var implicitlyAdded = new HashSet<BundleFile>(comparer);

            foreach (var file in files)
            {
                if (ExplicitOrder.Contains(file.VirtualFile.VirtualPath))
                {
                    explicitlyAdded.Add(file);
                }
                else
                {
                    implicitlyAdded.Add(file);
                }
            }

            var explicitlySorted = files.Intersect(explicitlyAdded, comparer);

            var dependencies = GetDependencyGraph(implicitlyAdded);
            var topo = new TopologicalSort<BundleFile>(implicitlyAdded, file => dependencies[file.VirtualFile.Name]);

            var implicitlySorted = topo.Sort();
            var sorted = explicitlySorted.Concat(implicitlySorted);

            return sorted;
        }

        private IDictionary<string, IEnumerable<BundleFile>> GetDependencyGraph(IEnumerable<BundleFile> files)
        {
            var filesByVirtualPath = files.ToDictionary(bundleFile => bundleFile.VirtualFile.VirtualPath, bundleFile => bundleFile);
            var graph = new Dictionary<string, IEnumerable<BundleFile>>();

            foreach (var file in files)
            {
                using (var stream = file.VirtualFile.Open())
                using (var reader = new StreamReader(stream))
                {
                    string content = reader.ReadToEnd();
                    var dependencies = GetBundleDependencies(content, file, filesByVirtualPath);

                    graph.Add(file.VirtualFile.Name, dependencies);
                }
            }

            return graph;
        }

        private IEnumerable<BundleFile> GetBundleDependencies(string content, BundleFile file, IDictionary<string, BundleFile> filesByVirtualPath)
        {
            var matches = regex.Matches(content);

            var virtualPaths = matches.OfType<Match>().Select(match => match.Groups["path"].Value);

            foreach (var virtualPath in virtualPaths)
            {
                if (filesByVirtualPath.ContainsKey(virtualPath))
                {
                    yield return filesByVirtualPath[virtualPath];
                }
                else if (!ExplicitOrder.Contains(virtualPath))
                {
                    string message = string.Format("'{0}' has a reference to a missing file '{1}'.", file.VirtualFile.VirtualPath, virtualPath);
                    throw new InvalidOperationException(message);
                }
            }
        }
    }
}