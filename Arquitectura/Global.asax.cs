using Model;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Arquitectura
{

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            if(ConfigurationManager.AppSettings["CreateDatabase"] == "1")
            {
                InitializeDatabase();
            }

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            Log4NetConfig.Configure(Server);
        }

        private static void InitializeDatabase()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, DbMigrationsConfiguration<Context>>());
            Context contextest = new Context();
            contextest.Database.Initialize(true);
        }

        public static class Log4NetConfig
        {
            public static void Configure(HttpServerUtility server)
            {
                var fileInfo = new FileInfo(server.MapPath("~/log4net.config"));
                log4net.Config.XmlConfigurator.ConfigureAndWatch(fileInfo);
            }
        }
    }
}