/* using System;
using System.Web.Mvc;
using System.Web.SessionState;

namespace lab_1_templates_sandu.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISessionManager _session; // Updated interface type

        public LoginController()
        {
            var b1 = new BussinessLogic();
            _session = b1.GetSessionBL();
        }

        // GET: Login
        public ActionResult LoginPage()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPage(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Credential = login.Credential,
                    Password = login.Password,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };
                var userLogin = _session.UserLogin(data); // Ensure ISessionManager has UserLogin method
                if (userLogin.Status)
                {
                    // ADD COOKIE
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userLogin.StatusMsg);
                    return View();
                }
            }
            return View();
        }
    }
}
*/