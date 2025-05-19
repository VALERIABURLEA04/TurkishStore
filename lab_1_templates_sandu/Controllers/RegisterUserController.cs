using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using lab_1_templates_sandu.Models;
using eUseControlBussinessLogic.Core;
using eUseControl.Domain.Entities.User;
using System;

namespace lab_1_templates_sandu.Controllers
{
    public class RegisterUserController : Controller
    {
        private readonly UserApi _userApi = new UserApi();

        public ActionResult RegisterPage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterPage(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserInfo
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = HashPassword(model.Password)
                };

                bool result = _userApi.RegisterUser(user);
                if (result)
                {
                    return RedirectToAction("RegisterSuccess");
                }

                ModelState.AddModelError("", "User with this email or username already exists.");
            }

            return View(model);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public ActionResult RegisterSuccess()
        {
            return View();
        }
    }
}
