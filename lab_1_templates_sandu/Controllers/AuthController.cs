using System;
using System.Web.Mvc;
using lab_1_templates_sandu.Models;
using eUseControl.Domain.Entities.User;
using BusinessLogic.DBModel;
using eUseControlBussinessLogic.Core;
using businessLogic.Interfaces;
using eUseControl.Domain.Entities.User.UserActionResponse;
using eUseControl.Domain.Enums;
using eUseControlBussinessLogic.Interfaces;
using eUseControlBussinessLogic;
using System.Web;
using lab_1_templates_sandu.Models.Auth;

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

        // GET: Account/Register
        public ActionResult Register()
        {
            ViewBag.HideFooter = true;
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserDataRegister registerModel)
        {
            if (ModelState.IsValid)
            {
                var data = new UserRegisterData
                {
                    Name = registerModel.Name,
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                    ConfirmPassword = registerModel.ConfirmPassword,
                    RegisterDataTime = DateTime.Now
                };

                try
                {
                    // call BL for token
                    string token = _register.SignUpLogic(data);

                    //if succes, else...
                }
                catch
                (Exception ex)
                {
                    // Handle exception (e.g., log it)
                    ModelState.AddModelError("", "An error occurred while registering. Please try again.");
                    return View(registerModel);
                }
                return RedirectToAction("Login");

            }

            return View(registerModel);
        }

        // GET: /Account/Login
        public ActionResult Login()
        {
            ViewBag.HideFooter = true;
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserDataLogin loginModel)
        {
            if (ModelState.IsValid)
            {
                var data = new UserLoginData
                {
                    NameOrEmail = loginModel.NameOrEmail,
                    Password = loginModel.Password,
                    LoginDataTime = DateTime.Now,
                };

                try
                {
                    // call BL for token
                    var userResp = _session.LogInLogic(data);

                    if (userResp.Status) //true
                    {
                        //Generarea unui cookie
                        UserCookieResp userCookieResp = _session.GenerateCookieByUser(userResp.UserId);

                        HttpCookie cookie = userCookieResp.Cookie;
                        Response.Cookies.Add(cookie);

                        Session["LoginStatus"] = "login";
                        Session["UserFirstName"] = userResp.Name ?? "User";

                        return RedirectToAction("Index", "Home");
                    }

                    else //false
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
                    // Eroare la procesarea cererii
                    TempData["ErrorMessage"] = "System error. Please try again later.";
                    // Loghează excepția pentru depanare
                    System.Diagnostics.Debug.WriteLine($"Error in SignUp: {ex.Message}");
                }
            }

            else
            {
                // Eroare pentru validarea mesajului
                TempData["ErrorMessage"] = "Invalid data. Try again.";
            }
            return View(loginModel);
        }

        public ActionResult Logout()
        {
            // Sterge cookie-ul de autentificare
            if (Request.Cookies["X-KEY"] != null)
            {
                var cookie = new HttpCookie("X-KEY");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            // Sterge sesiunea
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}