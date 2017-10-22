using AcademiaCerului.Core;
using AcademiaCerului.Models;
using System.Web.Mvc;

namespace AcademiaCerului.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public ViewResult Posts(int pageNo = 1)
        {
            var listViewModel = new ListViewModel(_blogRepository, pageNo);

            ViewBag.Title = "Latest Posts";

            return View("List", listViewModel);
        }
    }
}