using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLogic.DBModel;

namespace lab_1_templates_sandu.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Shop()
        {
            using (var db = new DataContext())
            {
                var products = db.Products.ToList(); // fetch all products
                ViewBag.IsAdmin = User.IsInRole("Admin");
                return View(products); // pass to the view
            }
        }

        // GET: Shop/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Shop"); // no ID, redirect to product list

            using (var db = new DataContext())
            {
                var product = db.Products.Find(id);
                if (product == null)
                    return RedirectToAction("Shop"); // not found, redirect to product list

                return View(product); // send product to Details view
            }
        }
    }
}