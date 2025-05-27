using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using businessLogic.BLStruct;
using businessLogic.Interfaces.Repositories;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Product;
using eUseControlBussinessLogic;

namespace eUseControl.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ShopController()
        {
            var businessLogic = new BusinesLogic();
            _productRepository = businessLogic.GetProductRepository(); // manually instantiate — no DI
        }

        // GET: Shop
        public ActionResult Shop()
        {
            var products = _productRepository.GetAllProducts();
            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(products);
        }

        public ActionResult ProductList()
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

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Product product, HttpPostedFileBase ImageUpload)
        {
            if (ModelState.IsValid)
            {
                _productRepository.AddProduct(product, ImageUpload);
                return RedirectToAction("Shop");
            }

            return View(product);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("Shop");

            var product = _productRepository.GetProductById(id.Value);
            if (product == null) return RedirectToAction("Shop");

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase ImageUpload, bool? RemoveImage)
        {
            _productRepository.UpdateProduct(product, ImageUpload, RemoveImage);
            return RedirectToAction("Edit", new { id = product.Id });
        }

        [HttpPost]
        public ActionResult DeleteImage(int id)
        {
            _productRepository.DeleteImage(id);
            return RedirectToAction("Edit", new { id = id });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("Shop");
        }
    }

}