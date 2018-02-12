using System.Web;
using System.Web.Optimization;

namespace Proyectores
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, consulte http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/light-bootstrap-dashboard.js",
                      "~/Scripts/bootstrap-notify.js",
                      "~/Scripts/bootstrap-select.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/light-bootstrap-dashboard.css",
                      "~/Content/pe-icon-7-stroke.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/roboto.css",
                      "~/Content/themes/base/jquery-ui.min.css",
                      "~/Content/themes/base/jquery-ui.css"
                      ));
        }
    }
}
