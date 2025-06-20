using eUSeControl.BusinessLogic.Dtos.ProductDtos;
using eUSeControl.BusinessLogic.Interfaces;
using eUSeControl.BusinessLogic.Services;
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
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public ShopController()
        {
            _productService = ProductService.GetInstance();
            _cartService = CartService.GetInstance();
        }

        // GET: /Shop/Index
        public ActionResult Index()
        {
            int userId = int.Parse(Session["UserId"]?.ToString() ?? "0");
            var products = _productService.GetAllProductsAsync(userId);
            ViewBag.IsAdmin = User.IsInRole("Admin");

            return View(products);
        }

        // GET: /Shop/Product
        public async Task<ActionResult> Product()
        {
            int userId = int.Parse(Session["UserId"]?.ToString() ?? "0");
            List<ProductDto> products = await _productService.GetAllProductsAsync(userId);

            ViewBag.IsAuthenticated = Session["LoginStatus"]?.ToString() == "login" ? "true" : "false";

            return View(products);
        }

        // GET: /Shop/ProductDetails/:id
        public async Task<ActionResult> ProductDetails(int? id)
        {
            if (id == null)
                return RedirectToAction("Product");

            var product = await _productService.GetProductDetailsByIdAsync(id.Value);
            if (product == null)
                return RedirectToAction("Product");

            return View(product);
        }

        // GET: /Shop/Add
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        // POST: /Shop/Add
        [HttpPost]
        public async Task<ActionResult> Add(UpsertProductDto model)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(model);
                return RedirectToAction("Shop");
            }

            return View(model);
        }

        // GET: /Shop/Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("Shop");

            var product = _productService.GetProductDetailsByIdAsync(id.Value);
            if (product == null) return RedirectToAction("Shop");

            return View(product);
        }

        // POST: /Shop/Edit
        [HttpPost]
        public ActionResult Edit(UpsertProductDto model)
        {
            _productService.UpdateProductAsync(model);
            return RedirectToAction("Edit", new { id = model.Id });
        }

        // POST: /Shop/Delete/:id
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _productService.DeleteProductAsync(id);
            return RedirectToAction("Shop");
        }

        // POST: /Shop/ToggleFavorite/:id
        [HttpPost]
        public async Task<ActionResult> ToggleFavorite(int id)
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

            bool isNowFav = await _productService.UpdateProductToFavoriteAsync(int.Parse(userId), id);
            return Json(new { isFavorite = isNowFav });
        }

        // GET: /Shop/Favorite
        [HttpGet]
        public async Task<ActionResult> Favorite()
        {
            var favoriteIds = new List<int>();

            if (Session["LoginStatus"]?.ToString() == "login")
            {
                var userId = Session["UserId"]?.ToString();
                if (!string.IsNullOrEmpty(userId))
                {
                    var dbFavs = await _productService.GetFavoriteProductIdsAsync(int.Parse(userId));
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
                                await _productService.UpdateProductToFavoriteAsync(int.Parse(userId), pid);
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
                favoritesList = await _productService.GetProductsByIdsAsync(favoriteIds, 0);
            }

            return View(favoritesList);
        }

        // GET: /Shop/FavoriteCount
        [ChildActionOnly]
        public ActionResult FavoriteCount()
        {
            int favCount = 0;
            if (Session["LoginStatus"]?.ToString() == "login")
            {
                var uid = int.Parse(Session["UserId"].ToString());
                favCount = _productService.GetFavoriteProductsCount(uid);
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

        [ChildActionOnly]
        public ActionResult CartCount()
        {
            int cartCount = 0;
            if (Session["LoginStatus"]?.ToString() == "login")
            {
                var uid = int.Parse(Session["UserId"].ToString());
                cartCount = _cartService.GetProductsFromCartByUserId(uid);
            }
            else
            {
                var cookie = Request.Cookies["productsInCart"];
                if (cookie != null)
                {
                    try
                    {
                        var list = JsonConvert.DeserializeObject<List<int>>(cookie.Value);
                        cartCount = list?.Count ?? 0;
                    }
                    catch { }
                }
            }

            return PartialView("_CartCount", cartCount);
        }
    }
}