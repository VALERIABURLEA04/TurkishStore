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

namespace lab_1_templates_sandu.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin Login
        public ActionResult AdminLogin()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            if (Session["AdminUsername"] == null)
                return RedirectToAction("AdminLogin");

            return View(); // This will look for Views/Admin/Dashboard.cshtml
        }


        public ActionResult ContactMessages()
        {
            if (Session["AdminUsername"] == null)
                return RedirectToAction("AdminLogin");

            var messages = _context.ContactData.ToList(); // or however you get your messages
            return View(messages);
        }

        public ActionResult Message(int id)
        {
            if (Session["AdminUsername"] == null)
                return RedirectToAction("AdminLogin");

            var message = _context.ContactData.FirstOrDefault(m => m.Id == id);
            if (message == null)
                return HttpNotFound();

            return View(message); // Make sure you have Message.cshtml
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
                     return RedirectToAction("Dashboard", "Admin");
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
                    builder.Append(b.ToString("x2"));
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
        public ActionResult Add(Product product, HttpPostedFileBase ImageUpload)
        {
            if (ModelState.IsValid)
            {
                // If an image was uploaded
                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    // Generate a unique file name to prevent conflicts
                    var fileName = Path.GetFileName(ImageUpload.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                    // Save path (change this if needed)
                    var savePath = Path.Combine(Server.MapPath("~/Content/images"), uniqueFileName);
                    ImageUpload.SaveAs(savePath);

                    // Set the relative URL
                    product.ImageUrl = "/Content/images/" + uniqueFileName;
                }

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
        public ActionResult Edit(Product product, HttpPostedFileBase ImageUpload, bool? RemoveImage)
        {
            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct == null)
            {
                return HttpNotFound();
            }

            // Update basic fields
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            // Handle image removal
            if (RemoveImage == true && !string.IsNullOrEmpty(existingProduct.ImageUrl))
            {
                // Optional: delete the physical file from server
                var fullPath = Server.MapPath(existingProduct.ImageUrl);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                // Remove reference from DB
                existingProduct.ImageUrl = null;
            }

            // Handle image upload
            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                // Generate unique file name and save
                var fileName = Path.GetFileName(ImageUpload.FileName);
                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(fileName);
                var path = Path.Combine(Server.MapPath("~/Content/images"), uniqueFileName);
                ImageUpload.SaveAs(path);

                // Optional: delete old image before replacing
                if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
                {
                    var oldPath = Server.MapPath(existingProduct.ImageUrl);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                existingProduct.ImageUrl = "/Content/images" + uniqueFileName;
            }

            _context.SaveChanges();

            return RedirectToAction("Edit", new { id = product.Id }); // show updated version
        }

        [HttpPost]
        public ActionResult DeleteImage(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                try
                {
                    var fullPath = Server.MapPath(product.ImageUrl);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                catch (Exception ex)
                {
                    // Optional: log the error
                    System.Diagnostics.Debug.WriteLine("Error deleting image: " + ex.Message);
                }

                // Clear the image from the DB
                product.ImageUrl = null;
                _context.SaveChanges();
            }

            return RedirectToAction("Edit", new { id = id });
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



/*
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
*/