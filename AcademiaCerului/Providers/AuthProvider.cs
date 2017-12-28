using System.Web;
using static System.Web.Security.FormsAuthentication;

namespace AcademiaCerului.Providers
{
    public class AuthProvider : IAuthProvider
    {
        public bool IsLoggedIn => HttpContext.Current.User.Identity.IsAuthenticated;

        public bool Login(string username, string password)
        {
            var result = Authenticate(username, password);

            if (result)
                SetAuthCookie(username, false);

            return result;
        }

        public void Logout()
        {
            SignOut();
        }
    }
}