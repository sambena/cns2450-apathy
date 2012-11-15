using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using Apathy.DAL;
using Apathy.Models;

namespace Apathy
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Envelope", action = "Index", id = UrlParameter.Optional },
                null,
                new string[] { "Apathy.Controllers" }
            );

        }

        protected void Application_Start()
        {
            Database.SetInitializer<BudgetContext>(new SampleData());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}