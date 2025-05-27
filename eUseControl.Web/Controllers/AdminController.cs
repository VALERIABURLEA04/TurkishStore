using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using businessLogic.DBModel;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Admin;
using lab_1_templates_sandu.Models.Admin;
using eUseControl.Domain.Entities.Product;
using businessLogic.Interfaces;
using eUseControlBussinessLogic;
using System.Threading.Tasks;
using lab_1_templates_sandu.Logic.Attributes;

namespace lab_1_templates_sandu.Controllers
{
    public class AdminController : Controller
    {
        private readonly IContact _contactBL;

        public AdminController()
        {
            var bl = new BusinesLogic();
            _contactBL = bl.GetContactBL();
        }

        // GET: Admin/Login
        public ActionResult AdminLogin()
        {
            return View();
        }

        // GET: Admin/Dashboard
        [isAdmin]
        public ActionResult Dashboard()
        {
            if (Session["AdminUsername"] == null)
                return RedirectToAction("AdminLogin"); 

            return View();
        }

        // GET: Admin/ContactMessages
        public async Task<ActionResult> ContactMessages()
        {
            if (Session["AdminUsername"] == null)
                return RedirectToAction("AdminLogin");

            var messages = await _contactBL.GetAllAsync();
            return View(messages);
        }

        // GET: Admin/Message/5
        public async Task<ActionResult> Message(int id)
        {
            if (Session["AdminUsername"] == null)
                return RedirectToAction("AdminLogin");

            var message = await _contactBL.GetByIdAsync(id);
            if (message == null)
                return HttpNotFound();

            return View(message);
        }

        // POST: Admin/DeleteContact/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteContact(int id)
        {
            if (Session["AdminUsername"] == null)
                return RedirectToAction("AdminLogin");

            var success = await _contactBL.DeleteAsync(id);
            if (!success)
                TempData["ErrorMessage"] = "Could not delete the contact message.";

            return RedirectToAction("ContactMessages");
        }

        [HttpPost]
        public ActionResult AdminLogin(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                Session["AdminUsername"] = username;
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }
        }


        // Optional: Logout method
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("AdminLogin");
        }

        // Optional: Example of a restricted admin page
        public ActionResult AdminOnlyPage()
        {
            if (Session["AdminUsername"] == null)
                return RedirectToAction("AdminLogin");

            return View();
        }

    }

}

        /*
        private readonly DataContext _context;

        public AdminController()
        {
            _context = new DataContext();
        }

        public ActionResult UserList()
        {
            if (Session["AdminUsername"] == null)
                return RedirectToAction("AdminLogin");

            using (var userDb = new UserContext())
            {
                var users = userDb.Users.Select(u => new AdminUserDisplay
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    LastLogin = u.LastLogin,
                    UserIp = u.UserIp,
                    Role = u.Level.ToString()
                }).ToList();

                return View(users);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id)
        {
            using (var userDb = new UserContext())
            {
                var user = userDb.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                userDb.Users.Remove(user);
                userDb.SaveChanges();

                TempData["SuccessMessage"] = "User deleted successfully.";
                return RedirectToAction("UserList");
            }
        }

    }
}
        */