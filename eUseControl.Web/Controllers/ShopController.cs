using businessLogic.Dtos.ProductDtos;
using businessLogic.Interfaces.Repositories;
using eUseControlBussinessLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            _productRepository = businessLogic.GetProductRepository();
        }

        // GET: Shop
        public ActionResult Shop()
        {
            int userId = int.Parse(Session["UserId"]?.ToString() ?? "0");
            var products = _productRepository.GetAllProducts(userId);
            ViewBag.IsAdmin = User.IsInRole("Admin");

            return View(products);
        }

        // GET: /Shop/Product
        public async Task<ActionResult> Product()
        {
            int userId = int.Parse(Session["UserId"]?.ToString() ?? "0");
            List<ProductDto> products = await _productRepository.GetAllProducts(userId);

            ViewBag.IsAuthenticated = Session["LoginStatus"]?.ToString() == "login" ? "true" : "false";

            return View(products);
        }

        // GET: /Shop/ProductDetails/:id
        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
                return RedirectToAction("Product");

            var product = _productRepository.GetProductDetailsById(id.Value);
            if (product == null)
                return RedirectToAction("Product");

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
            var userId = Session["UserId"]?.ToString();

            if (string.IsNullOrEmpty(userId))
            {
                var cookie = Request.Cookies["favorites"];
                var favs = cookie != null
                    ? JsonConvert.DeserializeObject<List<int>>(cookie.Value) ?? new List<int>()
                    : new List<int>();

                favs.Remove(id);

                var newCookie = new HttpCookie("favorites", JsonConvert.SerializeObject(favs))
                {
                    Expires = DateTime.Now.AddDays(30),
                    Path = "/"
                };
                Response.Cookies.Add(newCookie);

                return Json(new { isFavorite = false });
            }

            bool isNowFav = _productRepository.UpdateProductToFavorite(int.Parse(userId), id);
            return Json(new { isFavorite = isNowFav });
        }

        // GET: /Shop/Favorite
        [HttpGet]
        public ActionResult Favorite()
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

        [ChildActionOnly]
        public ActionResult FavoriteCount()
        {
            int favCount = 0;
            if (Session["LoginStatus"]?.ToString() == "login")
            {
                var uid = int.Parse(Session["UserId"].ToString());
                favCount = _productRepository.GetFavoriteProductIds(uid).Count();
            }
            else
            {
                var cookie = Request.Cookies["favorites"];
                if (cookie != null)
                {
                    try
                    {
                        var list = JsonConvert.DeserializeObject<List<int>>(cookie.Value);
                        favCount = list?.Count ?? 0;
                    }
                    catch { }
                }
            }
            return PartialView("_FavoriteCount", favCount);
        }
    }
}