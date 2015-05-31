using System.Web.Optimization;

namespace Site.Common
{
    public class JsRemoveSourceMappingUrlTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            if (response == null || string.IsNullOrWhiteSpace(response.Content))
            {
                return;
            }

            response.Content = response.Content.Replace("//# sourceMappingURL=", "//");
        }
    }
}