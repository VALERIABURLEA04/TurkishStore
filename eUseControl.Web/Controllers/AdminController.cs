using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using businessLogic.Interfaces;
using eUseControlBussinessLogic;
using eUseControl.Domain.Entities.Admin;
using eUseControl.Web.Logic.Attributes;

namespace ProjectOnlineStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IContact _contactBL;
        private readonly IAdmin _adminBL;

        public AdminController()
        {
            var bl = new BusinesLogic();
            _contactBL = bl.GetContactBL();
            _adminBL = bl.GetAdminBL();
        }

        // GET: Admin/Login
        public ActionResult AdminLogin()
        {
            if (Session["AdminUsername"] != null)
                return RedirectToAction("Dashboard");

            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        public ActionResult AdminLogin(string username, string password)
        {
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Login attempt: username = {username}, password = {password}");

            var admin = _adminBL.AdminLogin(username, password);

            if (admin != null)
            {
                Console.WriteLine($"[DEBUG] Login successful for user: {admin.Username}");
                Session["AdminUsername"] = admin.Username;
                return RedirectToAction("Dashboard");
            }
            else
            {
                Console.WriteLine("[DEBUG] Login failed: invalid username or password.");
                ViewBag.Error = "Invalid username or password.";
                return View();
            }
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

        // GET: Admin/Logout
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