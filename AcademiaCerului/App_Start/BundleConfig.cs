using System.Web;
using System.Web.Optimization;

namespace AcademiaCerului
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery", "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval", "http://ajax.aspnetcdn.com/ajax/jquery.validate/1.10.0/jquery.validate.min.js").Include(
                        "~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryunobtrusiveval", "http://ajax.aspnetcdn.com/ajax/mvc/3.0/jquery.validate.unobtrusive.min.js").Include(
                        "~/Scripts/jquery.validate.unobtrusive.js"));

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

            bundles.Add(new StyleBundle("~/Content/themes/simple/admin").Include(
                        "~/Content/themes/simple/admin.css"));

            bundles.Add(new StyleBundle("~/Scripts/jqgrid/css/bundle").Include(
                        "~/Scripts/jqgrid/css/ui.jqgrid.css"));

            bundles.Add(new StyleBundle("~/Content/themes/simple/jqueryuicustom/css/sunny/bundle").Include(
                        "~/Content/themes/simple/jqueryuicustom/css/sunny/jquery-ui-1.9.2.custom.css"));

            bundles.Add(new ScriptBundle("~/Scripts/tiny_mce/js").Include(
                        "~/Scripts/tiny_mce/tiny_mce.js"));

            bundles.Add(new ScriptBundle("~/manage/js").Include(
                        "~/Scripts/jqGrid/js/i18n/grid.locale-ro.js",
                        "~/Scripts/admin.js"));
        }
    }
}
