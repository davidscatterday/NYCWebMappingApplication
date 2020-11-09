using System.Web.Optimization;

namespace NYCMappingWebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.tablesorter.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/customJs").Include(
                        "~/Scripts/otherFunctions.js",
                        "~/Scripts/trendAnalysis.js",
                        "~/Scripts/consumerProfiles.js",
                        "~/Scripts/gridmvc.js",
                        "~/Scripts/select2-3.5.2.js",
                        "~/Scripts/gisFunctions.js"));

            bundles.Add(new ScriptBundle("~/bundles/emptyLayoutJavascript").Include(
                        "~/Scripts/emptyLayoutJS.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css",
                      "~/Content/headerStyle.css",
                      "~/Content/select2-3.5.2.css",
                      "~/Content/themes/base/accordion.css",
                      "~/Content/themes/base/autocomplete.css",
                      "~/Content/themes/base/base.css",
                      "~/Content/themes/base/button.css",
                      "~/Content/themes/base/core.css",
                      "~/Content/themes/base/datepicker.css",
                      "~/Content/themes/base/dialog.css",
                      "~/Content/themes/base/draggable.css",
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/themes/base/menu.css",
                      "~/Content/themes/base/progressbar.css",
                      "~/Content/themes/base/resizable.css",
                      "~/Content/themes/base/selectable.css",
                      "~/Content/themes/base/selectmenu.css",
                      "~/Content/themes/base/slider.css",
                      "~/Content/themes/base/sortable.css",
                      "~/Content/themes/base/spinner.css",
                      "~/Content/themes/base/tabs.css",
                      "~/Content/themes/base/theme.css",
                      "~/Content/themes/base/tooltip.css"));
        }
    }
}
