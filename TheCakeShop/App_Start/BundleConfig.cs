using System.Web;
using System.Web.Optimization;

namespace TheCakeShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Assets/css").Include(
                     "~/Assets/css/bootstrap.min.css",
                     "~/Assets/css/icons.css",
                     "~/Assets/css/metismenu.min.css",
                     "~/Assets/css/sweetalert2.min.css",
                     "~/Assets/css/style.css",
                     "~/Assets/css/animate.css",
                     "~/Assets/css/custom.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr2").Include(
                    "~/Assets/js/modernizr.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery2").Include(
                    "~/Assets/js/jquery.min.js",
                    "~/Assets/js/popper.min.js",
                    "~/Assets/js/bootstrap.min.js",
                    "~/Assets/js/metisMenu.min.js",
                    "~/Assets/js/waves.js",
                    "~/Assets/js/jquery.slimscroll.js",
                    "~/Assets/js/jquery.sweet-alert.init.js"
                ));

            bundles.Add(new ScriptBundle("~/Assets/dashboard").Include(
                    "~/Assets/pages/jquery.dashboard.init.js"
                ));

            bundles.Add(new ScriptBundle("~/Assets/appjs").Include(
                    "~/Assets/js/jquery.core.js",
                    "~/Assets/js/jquery.app.js"
                ));

            bundles.Add(new StyleBundle("~/FrontAssets/css").Include(
                     "~/FrontAssets/css/bootstrap.min.css",
                     "~/FrontAssets/css/magnific-popup.css",
                     "~/FrontAssets/css/font-icons.css",
                     "~/FrontAssets/css/sliders.css",
                     "~/FrontAssets/css/style.css",
                     "~/FrontAssets/css/animate.min.css"
                ));

            bundles.Add(new ScriptBundle("~/FrontAssets/js").Include(
                    "~/FrontAssets/js/jquery.min.js",
                    "~/FrontAssets/js/bootstrap.min.js",
                    "~/FrontAssets/js/plugins.js",
                    "~/FrontAssets/js/scripts.js"
                ));
        }
    }
}
