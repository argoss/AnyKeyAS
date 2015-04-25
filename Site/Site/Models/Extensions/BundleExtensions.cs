using System.Collections.Generic;
using System.Web.Optimization;

namespace Site.Models.Extensions
{
    public static class BundleExtensions
    {
        public static Bundle PreserveOrder(this Bundle bundle)
        {
            bundle.Orderer = new PreserveOrderBundleOrderer();

            return bundle;
        }

        public static void AddTo(this Bundle bundle, BundleCollection collection)
        {
            collection.Add(bundle);
        }

        private class PreserveOrderBundleOrderer : IBundleOrderer
        {
            public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
            {
                return files;
            }
        }
    }
}