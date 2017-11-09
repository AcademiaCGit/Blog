using AcademiaCerului.Core;
using AcademiaCerului.Models;
using System.Web;
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

        public ViewResult GetPostsByCategory(string category, int pageNo = 1)
        {
            var listViewModel = new ListViewModel(_blogRepository, category, "Category", pageNo);

            if (listViewModel.Category == null)
                throw new HttpException(404, "Categoria nu a fost gasita");

            ViewBag.Title = string.Format("Cele mai recente postari din categoria {0}", listViewModel.Category.Name);

            return View("List", listViewModel);
        }

        public ViewResult GetPostsByTag(string tag, int pageNo = 1)
        {
            var listViewModel = new ListViewModel(_blogRepository, tag, "Tag", pageNo);

            if (listViewModel.Tag == null)
                throw new HttpException(404, "Eticheta nu a fost gasita");

            ViewBag.Title = string.Format("Cele mai recente postari din eticheta {0}", listViewModel.Tag.Name);

            return View("List", listViewModel);
        }
    }
}