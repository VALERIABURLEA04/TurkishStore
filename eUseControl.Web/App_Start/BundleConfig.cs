using System.Web;
using System.Web.Optimization;

public class BundleConfig
{
	public static void RegisterBundles(BundleCollection bundles)
	{
		// Bundle pentru CSS
		bundles.Add(new StyleBundle("~/bundles/css").Include(
			  "~/Content/css/bootstrap.min.css", // Bootstrap
			  "~/Content/css/site.css",         // CSS-ul template-ului
			  "~/Content/css/style.css",
			  "~/Content/css/my_styles.css"));




         

        // Bundle pentru JS
        bundles.Add(new ScriptBundle("~/bundles/js").Include(
				  "~/Scripts/js/jquery-{version}.js",  // jQuery
				  "~/Scripts/js/bootstrap.bundle.min.js", // Bootstrap JS
				  "~/Scripts/js/custom.js"));          // Scripturi din template

		// Activează optimizările (se poate dezactiva pentru debugging)
		BundleTable.EnableOptimizations = true;
	}
}