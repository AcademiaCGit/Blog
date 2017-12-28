using AcademiaCerului.Models;
using AcademiaCerului.Providers;
using System.Web.Mvc;

namespace AcademiaCerului.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAuthProvider _authProvider;

        public AdminController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
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
    }
}