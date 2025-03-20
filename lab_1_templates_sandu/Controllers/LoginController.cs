using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace lab_1_templates_sandu.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISessionIDManager _session;
        public LoginController()
        {
            var b1 = new BussinesLogic();
            _session = b1.GetSessionBL();


        }
        // GET: Login
        public ActionResult LoginPage()
        {
            return View();
        }
    }
}
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
        var userLogin = _session.UserLogin(data);
        if (userLogin.Status)
        {
            //ADD COOKIE 

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