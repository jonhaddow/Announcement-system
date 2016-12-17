using System.Web;
using System.Web.Optimization;

namespace Coursework
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Bundle all global styles used in the application.
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/myBootstrapStyling.css",
                      "~/Content/site.css",
                      "~/Content/sweetalert.css",
                      "~/Content/materialicons.css"));

            // Bundle for jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            // Bundle for jquery validation library
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            // Bundle for bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // Bundle for global scripts
            bundles.Add(new ScriptBundle("~/bundles/globalScripts").Include(
                "~/Scripts/sweetalert.min.js"));
        }
    }
}
