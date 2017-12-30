using AcademiaCerului.Core;
using AcademiaCerului.Core.Objects;
using AcademiaCerului.Providers;
using Ninject;
using Ninject.Web.Common;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace AcademiaCerului
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(new RepositoryModule());
            kernel.Bind<IBlogRepository>().To<BlogRepository>();
            kernel.Bind<IAuthProvider>().To<AuthProvider>();

            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(Post), new PostModelBinder(Kernel));
            base.OnApplicationStarted();
        }
    }
}
