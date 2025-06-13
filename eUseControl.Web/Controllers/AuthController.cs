using businessLogic.Dtos.UserDtos;
using businessLogic.Interfaces;
using eUseControl.Domain.Enums;
using eUseControlBussinessLogic;
using eUseControlBussinessLogic.Interfaces;
using System;
using System.Web;
using System.Web.Mvc;

namespace ProjectOnlineStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISession _session;
        private readonly IRegister _register;

        public AuthController()
        {
            var bl = new BusinesLogic();
            _session = bl.GetSessionBL();
            _register = bl.GetRegisterBL();
        }

        // GET: /Auth/Register
        public ActionResult Register()
        {
            ViewBag.HideFooter = true;
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        public ActionResult Register(UserRegisterDto registerModel)
        {
            if (ModelState.IsValid)
            {
                var data = new UserRegisterDto
                {
                    Name = registerModel.Name,
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                    ConfirmPassword = registerModel.ConfirmPassword,
                    RegisterDataTime = DateTime.Now
                };

                try
                {
                    string token = _register.SignUpLogic(data);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "An error occurred while registering. Please try again.");
                    return View(registerModel);
                }
                return RedirectToAction("Login");
            }

            return View(registerModel);
        }

        // GET: /Auth/Login
        public ActionResult Login()
        {
            ViewBag.HideFooter = true;
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public ActionResult Login(UserLoginDto loginModel)
        {
            if (ModelState.IsValid)
            {
                var data = new UserLoginDto
                {
                    NameOrEmail = loginModel.NameOrEmail,
                    Password = loginModel.Password,
                    LoginDataTime = DateTime.Now,
                };

                try
                {
                    var userResp = _session.LogInLogic(data);

                    if (userResp.Status)
                    {
                        UserCookieRespDto userCookieResp = _session.GenerateCookieByUser(userResp.UserId);

                        HttpCookie cookie = userCookieResp.Cookie;
                        Response.Cookies.Add(cookie);

                        Session["UserId"] = userResp.UserId;
                        Session["LoginStatus"] = "login";
                        Session["UserFullName"] = userResp.Name ?? "Guest";
                        Session["Role"] = userResp.Role;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        switch (userResp.Result)
                        {
                            case LogInResult.EmailNotFound:
                                TempData["ErrorMessage"] = "This user does not exist.";
                                break;

                            case LogInResult.WrongPassword:
                                TempData["ErrorMessage"] = "Incorrect password.";
                                break;

                            default:
                                TempData["ErrorMessage"] = "Try again.";
                                break;
                        }
                        return View(loginModel);
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "System error. Please try again later.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid data. Try again.";
            }
            return View(loginModel);
        }

        // POST: /Auth/Logout
        [HttpPost]
        public ActionResult Logout()
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var cookie = new HttpCookie("X-KEY");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }

        // GET: /Auth/MyAccount
        [HttpGet]
        public ActionResult MyAccount()
        {
            if (Session["LoginStatus"] != null && Session["LoginStatus"].ToString() == "login")
            {
                var fullName = Session["UserFullName"]?.ToString() ?? "User";
                ViewBag.FullName = fullName;
                return View();
            }

            return RedirectToAction("Login");
        }
    }
}