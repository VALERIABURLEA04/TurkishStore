using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using businessLogic.BLStruct;
using businessLogic.Interfaces.Repositories;
using BusinessLogic.DBModel;

namespace lab_1_templates_sandu.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ShopController()
        {
            _productRepository = new ProductRepositoryBL(); // manually instantiate — no DI
        }

        // GET: Shop
        public ActionResult Shop()
        {
            var products = _productRepository.GetAllProducts();
            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(products);
        }

        // GET: Shop/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Shop");

            var product = _productRepository.GetProductById(id.Value);
            if (product == null)
                return RedirectToAction("Shop");

            return View(product);
        }
    }

}