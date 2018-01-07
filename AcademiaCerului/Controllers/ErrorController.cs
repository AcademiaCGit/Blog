using System.Web.Mvc;

namespace AcademiaCerului.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult NotFound()
        {
            return View();
        }
    }
}