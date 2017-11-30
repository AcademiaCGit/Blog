using AcademiaCerului.Core;
using AcademiaCerului.Models;
using System;
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

        public ViewResult Post(int year, int month, string title)
        {
            var post = _blogRepository.Post(year, month, title);

            if (post == null)
                throw new HttpException(404, "Postare negasită");

            if (post.Published == false && User.Identity.IsAuthenticated == false)
                throw new HttpException(401, "Postarea nu este publicată");

            return View(post); 
        }

        public ViewResult Posts(int pageNo = 1)
        {
            var listViewModel = new ListViewModel(_blogRepository, pageNo);

            ViewBag.Title = "Toate postarile";

            return View("PostsList", listViewModel);
        }

        public ViewResult GetPostsByCategory(string category, int pageNo = 1)
        {
            var listViewModel = new ListViewModel(_blogRepository, category, "Category", pageNo);

            if (listViewModel.Category == null)
                throw new HttpException(404, "Categoria nu a fost gasita");

            ViewBag.Title = string.Format("Cele mai recente postari din categoria {0}", listViewModel.Category.Name);

            return View("PostsList", listViewModel);
        }

        public ViewResult GetPostsByTag(string tag, int pageNo = 1)
        {
            var listViewModel = new ListViewModel(_blogRepository, tag, "Tag", pageNo);

            if (listViewModel.Tag == null)
                throw new HttpException(404, "Eticheta nu a fost gasita");

            ViewBag.Title = string.Format("Cele mai recente postari din eticheta {0}", listViewModel.Tag.Name);

            return View("PostsList", listViewModel);
        }

        public ViewResult GetPostsBySearch(string search, int pageNo = 1)
        {
            ViewBag.Title = "Lista de postari gasite dupa textul cautat";

            var viewModel = new ListViewModel(_blogRepository, search, "Search", pageNo);

            return View("PostsList", viewModel);
        }
    }
}