using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using log4net.Config;

namespace StreamSpike
{
    public class Global : HttpApplication
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Global));

        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            XmlConfigurator.Configure();
        }

        private void Application_Error(object sender, EventArgs e)
        {
            Log.Error("Application error", Server.GetLastError());
        }
    }
}