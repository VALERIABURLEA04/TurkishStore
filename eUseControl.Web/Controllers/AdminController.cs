using businessLogic.Interfaces;
using businessLogic.Interfaces.Repositories;
using eUseControl.Domain.Entities.Product;
using eUseControl.Web.Logic.Attributes;
using eUseControlBussinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IContact _contactBL;
        private readonly IProductRepository _productBL;

        public AdminController()
        {
            var bl = new BusinesLogic();

            _contactBL = bl.GetContactBL();
            _productBL = bl.GetProductRepository();
        }

        // GET: Admin/Login
        public ActionResult AdminLogin()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            List<Product> products = _productBL.GetAllProducts();
            return View(products);
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

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Role"] == null || Session["Role"]?.ToString() != "Admin")
            {
                filterContext.Result = new RedirectResult("~/Auth/Login");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}