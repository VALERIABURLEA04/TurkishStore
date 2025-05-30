using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using businessLogic.Interfaces;
using eUseControlBussinessLogic;
using eUseControl.Domain.Entities.Admin;
using eUseControl.Web.Logic.Attributes;
using eUseControl.Web.Logic.Mappers;
using eUseControl.Domain.Entities.Product;
using System.Collections.Generic;
using businessLogic.Interfaces.Repositories;
using eUseControl.Domain.Mappers;

namespace ProjectOnlineStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IContact _contactBL;
        private readonly IAdmin _adminBL;
        private readonly IProductRepository _productBL;

          public AdminController()
          {
             var bl = new BusinesLogic();
            _contactBL = bl.GetContactBL();
            _adminBL = bl.GetAdminBL();
            _productBL = bl.GetProductRepository();
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

            var entities = await _contactBL.GetAllAsync();
            var models = entities.Select(ContactMapper.ToModel).ToList();

            return View(models);
        }

        // GET: Admin/Message/5
        public async Task<ActionResult> Message(int id)
        {
            if (Session["AdminUsername"] == null)
                return RedirectToAction("AdminLogin");

            var entity = await _contactBL.GetByIdAsync(id);
            if (entity == null)
                return HttpNotFound();

            var model = ContactMapper.ToModel(entity);
            return View(model);
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

          public ActionResult ProductList()
          {
               List<ProductDataEntities> products = _productBL.GetAllProducts();
               var viewProducts = ProductMapper.ToViewModelList(products);
               return View(viewProducts);
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