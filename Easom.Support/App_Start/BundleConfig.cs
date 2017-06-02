using System.Web;
using System.Web.Optimization;

namespace Easom.Support.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                             "~/files/vendor/jquery/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/files/vendor/jquery/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/files/css/animate.css",
                        "~/files/css/font-awesome.min.css",
                        "~/files/css/simple-line-icons.css",
                        "~/files/css/font.css",
                        "~/files/css/app.css"));
        }
    }
}
