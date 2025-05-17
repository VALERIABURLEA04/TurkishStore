using System;
using System.Web.Mvc;
using lab_1_templates_sandu.Models;
using eUseControl.Domain.Entities.User;
using BusinessLogic.DBModel;
using eUseControlBussinessLogic.Core;

namespace ProjectOnlineStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserApi _userApi = new UserApi();

        // GET: Auth/login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Auth/login
        [HttpPost]
        [ValidateAntiForgeryToken] // Protecție CSRF
        public ActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new UserInfo
                {
                    UsernameUS = model.username,
                    PasswordUs = model.password
                };

                var result = _userApi.RegisterUser(newUser);


                if (result)
                {
                    // Redirecționează către pagina Home, dacă datele au fost salvate cu succes
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Redirecționează către pagina About, dacă apare o eroare la salvare
                    ViewBag.ErrorMessage = "A apărut o eroare la salvarea utilizatorului.";
                    return RedirectToAction("About", "Home");
                }
            }

            // Dacă ModelState nu este valid, rămâne pe pagina curentă pentru a corecta erorile de validare
            return View(model);
        }
    }
}