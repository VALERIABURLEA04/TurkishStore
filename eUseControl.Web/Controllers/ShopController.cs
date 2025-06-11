using businessLogic.Dtos.ProductDtos;
using businessLogic.Interfaces.Repositories;
using eUseControlBussinessLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            int userId = int.Parse(Session["UserId"]?.ToString() ?? "0");
            var products = _productRepository.GetAllProducts(userId);
            ViewBag.IsAdmin = User.IsInRole("Admin");

            return View(products);
        }

        public ActionResult ProductList()
        {
            int userId = int.Parse(Session["UserId"]?.ToString() ?? "0");

            List<ProductDto> products = _productRepository.GetAllProducts(userId);
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
        public ActionResult Add(ProductDto product, HttpPostedFileBase ImageUpload)
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
        public ActionResult Edit(ProductDto product, HttpPostedFileBase ImageUpload, bool? RemoveImage)
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

        [HttpPost]
        public ActionResult ToggleFavorite(int id)
        {
            string userId = Session["UserId"]?.ToString();
            if (userId == null)
                return View("Auth/Register");

            var result = _productRepository.UpdateProductToFavorite(int.Parse(userId), id);

            return Json(new { isFavorite = result });
        }

        [HttpGet]
        public ActionResult Favorites()
        {
            var favoriteIds = new List<int>();

            if (Session["LoginStatus"]?.ToString() == "login")
            {
                var userId = Session["UserId"]?.ToString();
                if (!string.IsNullOrEmpty(userId))
                {
                    var dbFavs = _productRepository.GetFavoriteProductIds(int.Parse(userId));
                    favoriteIds.AddRange(dbFavs);

                    HttpCookie cookie = Request.Cookies["favorites"];
                    if (cookie != null)
                    {
                        List<int> cookieFavs;
                        try
                        {
                            cookieFavs = JsonConvert.DeserializeObject<List<int>>(cookie.Value) ?? new List<int>();
                        }
                        catch
                        {
                            cookieFavs = new List<int>();
                        }

                        foreach (int pid in cookieFavs)
                        {
                            if (!favoriteIds.Contains(pid))
                            {
                                _productRepository.UpdateProductToFavorite(int.Parse(userId), pid);
                                favoriteIds.Add(pid);
                            }
                        }

                        var expiredCookie = new HttpCookie("favorites")
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            Path = "/"
                        };

                        Response.Cookies.Add(expiredCookie);
                    }
                }
            }
            else
            {
                HttpCookie cookie = Request.Cookies["favorites"];
                if (cookie != null)
                {
                    try
                    {
                        favoriteIds = JsonConvert.DeserializeObject<List<int>>(cookie.Value) ?? new List<int>();
                    }
                    catch
                    {
                        favoriteIds = new List<int>();
                    }
                }
            }

            List<ProductDto> favoritesList = new List<ProductDto>();
            if (favoriteIds.Any())
            {
                favoritesList = _productRepository.GetProductsByIds(favoriteIds);
            }

            return View(favoritesList);
        }
    }
}