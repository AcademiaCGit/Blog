using Ninject.Web.Common;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using AcademiaCerului.Core;

namespace AcademiaCerului
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            base.OnApplicationStarted();
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(new RepositoryModule());
            kernel.Bind<IBlogRepository>().To<BlogRepository>();

            return kernel;
        }
    }
}
