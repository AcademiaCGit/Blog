using AcademiaCerului.Core;
using AcademiaCerului.Models;
using AcademiaCerului.Providers;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace AcademiaCerului.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAuthProvider _authProvider;
        private readonly IBlogRepository _blogRepository;

        public AdminController(IAuthProvider authProvider, IBlogRepository blogRepository)
        {
            _authProvider = authProvider;
            _blogRepository = blogRepository;
        }

        //[RequireHttps]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (_authProvider.IsLoggedIn)
                return RedirectToUrl(returnUrl);

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        //[RequireHttps]
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && _authProvider.Login(model.UserName, model.Password))
                return RedirectToUrl(returnUrl);

            ModelState.AddModelError("", "Utilizatorul sau parola este sunt incorecte");
            return View(model);
        }

        private ActionResult RedirectToUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Manage");
        }

        public ActionResult Logout()
        {
            _authProvider.Logout();

            return RedirectToAction("Login", "Admin");
        }

        public ActionResult Manage()
        {
            return View();
        }

        public ContentResult Posts(GridManagerViewModel model)
        {
            var posts = _blogRepository.Posts(model.page - 1, model.rows, model.sidx, model.sord == "asc");
            var totalPosts = _blogRepository.TotalPosts(false);

            return Content(JsonConvert.SerializeObject(new
            {
                page = model.page,
                records = totalPosts,
                rows = posts,
                total = Math.Ceiling(Convert.ToDouble(totalPosts) / model.rows)
            }, new CustomDateTimeConverter()), "application/json");
        }
    }
}