using System.Web;
using System.Web.Optimization;

namespace IAUECProfessorsEvaluation.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/kendo/kendo.all.min.js",
                "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                "~/Content/kendo/kendo.common-bootstrap.min.css",
                "~/Content/kendo/kendo.bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

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

            bundles.Add(new StyleBundle("~/Content/css/blank").Include(
                "~/Content/css/BlankLayout.css"
                ));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
               "~/Content/MdBootstrapPersianDateTimePicker/bootstrap-theme.css",
               "~/Content/MdBootstrapPersianDateTimePicker/jquery.Bootstrap-PersianDateTimePicker.css"
               ));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                    "~/Scripts/MdBootstrapPersianDateTimePicker/calendar.js",
                    "~/Scripts/MdBootstrapPersianDateTimePicker/jquery.Bootstrap-PersianDateTimePicker.js"));
            BundleTable.EnableOptimizations = false;
        }
    }
}
