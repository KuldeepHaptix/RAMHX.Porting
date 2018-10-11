using System.Web;
using System.Web.Optimization;

namespace RAMHX.CMS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Areas/Admin/Assets/js/jquery-1.10.2.min.js",
                        "~/Areas/Admin/Assets/js/bootbox.js",
                            "~/Areas/Admin/Assets/js/jquery.blockUI.js"
                        )
                );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Areas/Admin/Assets/js/jquery.validate.*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Areas/Admin/Assets/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Areas/Admin/Assets/js/bootstrap.min.js",
                      "~/Areas/Admin/Assets/js/respond.js",
                      "~/Areas/Admin/Assets/js/jquery-ui.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Areas/Admin/Assets/css/jquery-ui.css",
                      "~/Areas/Admin/Assets/bootstrap.css",
                      "~/Areas/Admin/Assets/css/jquery.dataTables.min.css",
                      "~/Areas/Admin/Assets/site.css",
                      "~/Areas/Admin/Assets/timepicki.css",
                      "~/Areas/Admin/Assets/css/toastr.min.css"
                      )
                      );

            bundles.Add(new ScriptBundle("~/bundles/bottomjs").Include(
                      "~/Areas/Admin/Assets/js/jquery.dataTables.min.js",
                      "~/Areas/Admin/Assets/js/common.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/toastrjs").Include(
                "~/Areas/Admin/Assets/js/toastr.min.js"
               ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
