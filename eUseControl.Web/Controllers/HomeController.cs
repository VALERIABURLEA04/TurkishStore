using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLogic.DBModel;
using businessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using eUseControlBussinessLogic.Interfaces;
using eUseControlBussinessLogic;
using businessLogic.BLStruct;
using lab_1_templates_sandu.Logic.Attributes;

namespace ProjectOnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContact _contactBL;
        private readonly ISession _sessionBL;

        // Parameterless constructor manually instantiating dependencies
        public HomeController()
        {
            var bl = new BusinesLogic();
            _contactBL = bl.GetContactBL();
            _sessionBL = bl.GetSessionBL();
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

        public ActionResult RegisterPage()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult RefundPage()
        {
            return View();
        }

        // POST: Home/ContactUs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ContactUs(UContactData contactData)
        {
            if (ModelState.IsValid)
            {
                await _contactBL.AddAsync(contactData);
                TempData["SuccessMessage"] = "Your message has been sent successfully!";
                return RedirectToAction("ContactUs");
            }
            return View(contactData);
        }

        /*
        [HttpPost]
        [isAdmin]
        public async Task<ActionResult> DeleteContact(int id)
        {
            var success = await _contactBL.DeleteAsync(id);
            if (!success)
                TempData["ErrorMessage"] = "Could not delete contact data.";

            return RedirectToAction("Dashboard", "Admin");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        */
    }
}
