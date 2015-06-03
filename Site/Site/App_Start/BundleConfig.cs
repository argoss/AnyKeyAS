using System.Linq;
using System.Web.Optimization;
using Site.Common;
using Site.Models.Extensions;

namespace Site
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                      "~/Content/styles/main-menu.css"));

            new ScriptBundle("~/scriptbundles/bootstrap").
                Include("~/Scripts/bootstrap.js").
                Include("~/Scripts/bootstrap-datepicker.js").
                AddTo(BundleTable.Bundles);

            new ScriptBundle("~/scriptbundles/jquery").
                Include("~/Scripts/jquery-{version}.js").
                Include("~/Scripts/jquery.validate.js").
                Include("~/Scripts/jquery.validate.unobtrusive.js").
                AddTo(BundleTable.Bundles);

            new ScriptBundle("~/scriptbundles/bootstrap").
				Include("~/Scripts/bootstrap.js").
				Include("~/Scripts/bootstrap.validate.js").
				Include("~/Scripts/bootstrap-datepicker.js").
				Include("~/Scripts/bootstrap-notify.js").
                AddTo(BundleTable.Bundles);

            new StyleBundle("~/stylebundles/bootstrap").
                Include("~/Content/bootstrap.css").
                Include("~/Content/bootstrap-theme.css").
                Include("~/Content/bootstrap-datepicker3.css").
                Include("~/Content/bootstrap-notify.css").
                AddTo(bundles);

            new ScriptBundle("~/scriptbundles/angular").
                Include("~/Scripts/angular.js").
                Include("~/Scripts/angular-animate.js").
                Include("~/Scripts/angular-resource.js").
                Include("~/Scripts/angular-route.js").
                Include("~/Scripts/angular-sanitize.min.js").
                AddTo(bundles);

            new ScriptBundle("~/scriptbundles/main-menu").
                Include("~/Scripts/MainMenu/main-menu.js").
                Include("~/Scripts/Users/user-list.js").
                AddTo(bundles);

            BundleTable.Bundles.ToList()
                .FindAll(x => x.GetType() == typeof(ScriptBundle))
                .ForEach(b => b.Transforms.Add(new JsRemoveSourceMappingUrlTransform()));
        }
    }
}
