using System.Web;
using System.Web.Optimization;

namespace Digital.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));



    //         <link href="style/bootstrap.css" rel="stylesheet">
    //<!-- Font awesome icon -->
    //<link rel="stylesheet" href="style/font-awesome.css">
    //<!-- jQuery UI -->
    //<link rel="stylesheet" href="style/jquery-ui.css">
    //<!-- Calendar -->
    //<link rel="stylesheet" href="style/fullcalendar.css">
    //<!-- prettyPhoto -->
    //<link rel="stylesheet" href="style/prettyPhoto.css">
    //<!-- Star rating -->
    //<link rel="stylesheet" href="style/rateit.css">
    //<!-- Date picker -->
    //<link rel="stylesheet" href="style/bootstrap-datetimepicker.min.css">
    //<!-- CLEditor -->
    //<link rel="stylesheet" href="style/jquery.cleditor.css">
    //<!-- Uniform -->
    //<link rel="stylesheet" href="style/uniform.default.css">
    //<!-- Bootstrap toggle -->
    //<link rel="stylesheet" href="style/bootstrap-switch.css">
    //<!-- Main stylesheet -->
    //<link href="style/style.css" rel="stylesheet">
    //<!-- Widgets stylesheet -->
    //<link href="style/widgets.css" rel="stylesheet">
            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/style/bootstrap.css",
                      "~/style/font-awesome.css",
                      "~/style/jquery-ui.css",
                      "~/style/fullcalendar.css",
                      "~/style/prettyPhoto.css",
                      "~/style/rateit.css",
                      "~/style/bootstrap-datetimepicker.min.css",
                      "~/style/jquery.cleditor.css",
                      "~/style/uniform.default.css",
                      "~/style/bootstrap-switch.css",
                      "~/style/style.css",
                      "~/style/widgets.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/js/bootstrap.js",
                        "~/js/jquery.js",
                        "~/js/jquery-ui-1.9.2.custom.min.js",
                        "~/js/fullcalendar.min.js",
                         "~/js/jquery.rateit.min.js",
                          "~/js/prettyPhoto.js"
                        )
                        );
            bundles.Add(new ScriptBundle("~/bundles/Flot").Include(
                       "~/js/excanvas.min.js",
                       "~/js/jquery.flot.js",
                       "~/js/jquery.flot.resize.js",
                       "~/js/jquery.flot.pie.js",
                        "~/js/jquery.flot.stack.js"
                       )
                       );
            bundles.Add(new ScriptBundle("~/bundles/Noty").Include(
                       "~/js/jquery.noty.js",
                       "~/js/themes/default.js",
                       "~/js/layouts/bottom.js",
                       "~/js/layouts/topRight.js",
                        "~/js/layouts/top.js"
                       )
                       );
            bundles.Add(new ScriptBundle("~/bundles/Common").Include(
                      "~/js/sparklines.js",
                      "~/js/jquery.cleditor.min.js",
                      "~/js/JqueryToken.js",
                      "~/js/bootstrap-datetimepicker.min.js",
                      "~/js/jquery.uniform.min.js",
                       "~/js/bootstrap-switch.min.js",
                       "~/js/jquery.prettyPhoto.js",
                       "~/js/filter.js",
                       "~/js/custom.js",
                       "~/js/charts.js"
                      )
                      );
            BundleTable.EnableOptimizations = true;
        }
    }
}
