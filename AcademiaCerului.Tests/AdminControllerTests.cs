using AcademiaCerului.Controllers;
using AcademiaCerului.Core;
using AcademiaCerului.Models;
using AcademiaCerului.Providers;
using NUnit.Framework;
using Rhino.Mocks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AcademiaCerului.Tests
{
    [TestFixture]
    public class AdminControllerTests
    {
        private AdminController _adminController;
        private IAuthProvider _authProvider;
        private IBlogRepository _blogRepository;

        [SetUp]
        public void SetUp()
        {
            _authProvider = MockRepository.GenerateMock<IAuthProvider>();
            _blogRepository = MockRepository.GenerateMock<IBlogRepository>();
            _adminController = new AdminController(_authProvider, _blogRepository);

            var httpContextMock = MockRepository.GenerateMock<HttpContextBase>();
            _adminController.Url = new UrlHelper(new RequestContext(httpContextMock, new RouteData()));
        }

        [Test]
        public void Login_IsLogedIn__True_Test()
        {
            // arange
            _authProvider.Stub(s => s.IsLoggedIn).Return(true);

            // act
            var controller = _adminController.Login("/admin/manage");

            // assert
            Assert.IsInstanceOf<RedirectResult>(controller);
            Assert.AreEqual("/admin/manage", ((RedirectResult)controller).Url);
        }

        [Test]
        public void Login_IsLoggedIn_False_Test()
        {
            _authProvider.Stub(s => s.IsLoggedIn).Return(false);

            var controller = _adminController.Login("/admin/manage");

            Assert.IsInstanceOf<ViewResult>(controller);
            Assert.AreEqual("/admin/manage", ((ViewResult)controller).ViewBag.ReturnUrl);
        }

        [Test]
        public void Login_Post_Model_Invalid_Test()
        {
            var model = new LoginModel();
            _adminController.ModelState.AddModelError("UserName", "UserName is required");

            var controller = _adminController.Login(model, "/");

            Assert.IsInstanceOf<ViewResult>(controller);
        }

        [Test]
        public void Login_Post_User_Invalid_Test()
        {
            var model = new LoginModel
            {
                UserName = "invaliduser",
                Password = "password"
            };
            _authProvider.Stub(s => s.Login(model.UserName, model.Password)).Return(false);

            var controller = _adminController.Login(model, "/");

            Assert.IsInstanceOf<ViewResult>(controller);
            var modelStateErrors = _adminController.ModelState[""].Errors;
            Assert.IsTrue(modelStateErrors.Count > 0);
            Assert.AreEqual("Utilizatorul sau parola este sunt incorecte", modelStateErrors[0].ErrorMessage);
        }

        [Test]
        public void Login_Post_User_Valid_Test()
        {
            var model = new LoginModel
            {
                UserName = "validuser",
                Password = "password"
            };
            _authProvider.Stub(s => s.Login(model.UserName, model.Password)).Return(true);

            var controller = _adminController.Login(model, "/");

            Assert.IsInstanceOf<RedirectResult>(controller);
            Assert.AreEqual("/", ((RedirectResult)controller).Url);
        }
    }
}
