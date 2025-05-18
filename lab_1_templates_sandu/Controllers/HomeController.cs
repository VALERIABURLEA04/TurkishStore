using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.User;


namespace ProjectOnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private DataContext _context; // Declare your database context here

        public HomeController()
        {
            _context = new DataContext(); // Initialize your context
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
            }
        
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult NewIn()
        {
            return View();
        }

        // POST: Home/ContactUs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUs(UContactData contactData)
        {
            if (ModelState.IsValid)
            {
                // Save the contact data to the database
                _context.ContactData.Add(contactData);
                _context.SaveChanges();

                // Set the success message in TempData
                TempData["SuccessMessage"] = "Your message has been sent successfully!";
                return RedirectToAction("ContactUs");
            }

            // Return the view with validation errors if model state is not valid
            return View(contactData);
        }

        // Dispose the context to avoid memory leaks
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}