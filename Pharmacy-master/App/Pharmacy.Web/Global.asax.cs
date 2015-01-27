using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using FluentValidation.Mvc;
using Pharmacy.IoC.CastleWindsor.Installers;
using Pharmacy.Web.App_Start;

namespace Pharmacy.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new WindsorContainer();
            container.Install(new AdminInstaller());

            var automapper = new AutoMapperConfig();
            automapper.Init();

            FluentValidationModelValidatorProvider.Configure();
        }
    }
}
