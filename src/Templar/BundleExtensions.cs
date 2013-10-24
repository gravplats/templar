using System.Web.Optimization;

namespace Templar
{
    public static class BundleExtensions
    {
        public static TBundle AddTo<TBundle>(this TBundle bundle, BundleCollection collection)
            where TBundle : Bundle
        {
            collection.Add(bundle);
            return bundle;
        }
    }
}