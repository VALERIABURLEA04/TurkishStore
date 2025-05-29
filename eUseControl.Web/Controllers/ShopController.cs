using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using businessLogic.BLStruct;
using businessLogic.Interfaces.Repositories;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Mappers;
using eUseControl.Web.Models.Product;
using eUseControlBussinessLogic;

namespace lab_1_templates_sandu.Controllers
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
            var businessProducts = _productRepository.GetAllProducts(); // returns List<ProductDataEntities>
            var viewModels = ProductMapper.ToViewModelList(businessProducts); // map to view models
            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(viewModels);
        }

        public ActionResult ProductList()
        {
            var businessProducts = _productRepository.GetAllProducts();
            var viewModels = ProductMapper.ToViewModelList(businessProducts);
            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(viewModels);
        }

   
        // GET: Shop/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Shop");

            var productEntity = _productRepository.GetProductById(id.Value);  // business entity
            if (productEntity == null)
                return RedirectToAction("Shop");

            var productViewModel = ProductMapper.ToViewModel(productEntity);  // map to view model
            return View(productViewModel);
        }


        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ProductDataEntities product, HttpPostedFileBase ImageUpload)
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
            if (id == null)
                return RedirectToAction("Shop");

            var product = _productRepository.GetProductById(id.Value);
            if (product == null)
                return RedirectToAction("Shop");

            var model = ProductMapper.ToViewModel(product);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel productModel, HttpPostedFileBase ImageUpload, bool? RemoveImage)
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, return the view with the current model so user can fix errors
                return View(productModel);
            }

            var productEntity = ProductMapper.ToBusinessEntity(productModel);
            _productRepository.UpdateProduct(productEntity, ImageUpload, RemoveImage);

            // Redirect back to Edit page with updated data
            return RedirectToAction("Edit", new { id = productModel.Id });
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
            return RedirectToAction("ProductList");
        }
    }

}