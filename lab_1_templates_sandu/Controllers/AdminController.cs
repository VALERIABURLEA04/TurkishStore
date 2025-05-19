using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Admin;

namespace lab_1_templates_sandu.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin Login
        public ActionResult AdminLogin()
        {
            return View();
        }

        /* [HttpPost]
        public ActionResult AdminLogin(string username, string password)
        {
            using (var db = new DataContext())
            {
                var hashedPassword = HashPassword(password);

                var admin = db.AdminData
                    .FirstOrDefault(a => a.Username == username && a.PasswordHash == hashedPassword);

                if (admin != null)
                {
                    Session["AdminUsername"] = admin.Username;

                    // Redirect to homepage instead of a dashboard
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Invalid username or password.";
                    return View();
                }
            }
        }
        */

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

        // Reusable method for password hashing
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString();
            }
        }

        // Method to create a new admin
        public void CreateAdmin(string username, string password)
        {
            using (var db = new DataContext())
            {
                var admin = new AdminData
                {
                    Username = username,
                    PasswordHash = HashPassword(password)
                };

                db.AdminData.Add(admin);
                db.SaveChanges();
            }
        }

        
    private readonly DataContext _context;

        public AdminController()
        {
            _context = new DataContext();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            var products = _context.Products.ToList();
            return View(products);
        }


        [HttpPost]
        public ActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("ProductList");
            }
            return View(product);
        }

        private DataContext db = new DataContext();

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                // Redirect to Product List (or whatever your main page is)
                return RedirectToAction("ProductList");
            }

            var product = db.Products.Find(id.Value);
            if (product == null)
            {
                // Also redirect if product not found
                return RedirectToAction("ProductList");
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("ProductList");
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("ProductList");
        }



    }
}
