using System.Web;
using System.Web.Optimization;

namespace fyp_One
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
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Scripts/classie.js",
                      "~/Scripts/custom.js",
                      "~/Scripts/google_map.js",
                      "~/Scripts/hoverIntent.js",
                      "~/Scripts/jquery-2.1.4.min.js",
                      "~/Scripts/jquery.countTo.js",
                      "~/Scripts/jquery.flexslider-min.js",
                      "~/Scripts/jquery.stellar.min.js",
                      "~/Scripts/jquery.waypoints.min.js",
                      "~/Scripts/modernizr-2.6.2.min.js",
                      "~/Scripts/owl.carousel.min.js",
                      "~/Scripts/selectFx.js",
                      "~/Scripts/superfish.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/style.css",
                      "~/Content/bootstrap-datepicker.min.css",
                      "~/Content/cs-select.css",
                      "~/Content/cs-skin-border.css",
                      "~/Content/flaticon.css",
                      "~/Content/flexslider.css",
                      "~/Content/icomoon.css",
                      "~/Content/owl.carousel.css",
                      "~/Content/owl.theme.default.min.css",
                      "~/Content/style.css.map",
                      "~/Content/superfish.css",
                      "~/Content/themify-icons.css"
                     
                                            ));
        }
    }
}
