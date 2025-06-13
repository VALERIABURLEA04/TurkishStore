using System.Web.Optimization;

public class BundleConfig
{
    public static void RegisterBundles(BundleCollection bundles)
    {
        // CSS Bundle
        bundles.Add(new StyleBundle("~/bundles/css").Include(
              "~/Content/css/bootstrap/css/bootstrap.min.css",
              "~/Content/css/fonts/font-awesome-4.7.0/css/font-awesome.min.css",
              "~/Content/fonts/iconic/css/material-design-iconic-font.min.css",
              "~/Content/fonts/linearicons-v1.0.0/icon-font.min.css",
              "~/Content/css/animate/animate.css",
              "~/Content/css/css-hamburgers/hamburgers.min.css",
              "~/Content/css/animsition/css/animsition.min.css",
              "~/Content/css/select2/select2.min.css",
              "~/Content/css/daterangepicker/daterangepicker.css",
              "~/Content/css/slick/slick.css",
              "~/Content/css/MagnificPopup/magnific-popup.css",
              "~/Content/css/perfect-scrollbar/perfect-scrollbar.css",
              "~/Content/css/util.css",
              "~/Content/css/main.css"));

        // JS Bundle
        bundles.Add(new ScriptBundle("~/bundles/js").Include(
                  "~/Scripts/jquery/jquery-3.2.1.min.js",
                  "~/Scripts/animsition/js/animsition.min.js",
                  "~/Scripts/bootstrap/js/popper.js",
                  "~/Scripts/bootstrap/js/bootstrap.min.js",
                  "~/Scripts/select2/select2.min.js",
                  "~/Scripts/daterangepicker/moment.min.js",
                  "~/Scripts/daterangepicker/daterangepicker.js",
                  "~/Scripts/slick/slick.min.js",
                  "~/Scripts/js/slick-custom.js",
                  "~/Scripts/parallax100/parallax100.js",
                  "~/Scripts/MagnificPopup/jquery.magnific-popup.min.js",
                  "~/Scripts/isotope/isotope.pkgd.min.js",
                  "~/Scripts/sweetalert/sweetalert.min.js",
                  "~/Scripts/js/main.js"));

        // Enable optimizations
        BundleTable.EnableOptimizations = true;
    }
}