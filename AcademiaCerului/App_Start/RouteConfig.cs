using System.Web.Mvc;
using System.Web.Routing;

namespace AcademiaCerului
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Post",
            //    "Archive/{year}/{month}/{title}",
            //    new { controller = "Blog", action = "Post" },
            //    new { year = @"\d{4}", month = @"\d{2}", day = @"\d{2}" }
            //    );

            routes.MapRoute(
                "Category",
                "Category/{category}",
                new { controller = "Blog", action = "GetPostsByCategory" }
                );

            routes.MapRoute(
                "Tag",
                "Tag/{tag}",
                new { controller = "Blog", action = "GetPostsByTag" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
