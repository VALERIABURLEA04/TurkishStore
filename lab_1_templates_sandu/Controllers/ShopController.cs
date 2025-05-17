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
    }
}
