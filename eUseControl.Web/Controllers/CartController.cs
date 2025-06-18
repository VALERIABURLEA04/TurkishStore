using businessLogic.Dtos.CartDtos;
using eUSeControl.BusinessLogic.Interfaces;
using eUSeControl.BusinessLogic.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectOnlineStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController()
        {
            _cartService = CartService.GetInstance();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddProductToCart(UpsertCartItemDto model)
        {
            if (Session["LoginStatus"]?.ToString() != "login")
                return RedirectToAction("Login", "Auth");

            model.UserId = int.Parse(Session["UserId"]?.ToString() ?? "0");

            var message = await _cartService.AddCartItemAsync(model);
            TempData["CartMessage"] = message;

            return RedirectToAction("Product", "Shop");
        }

        [HttpGet]
        public async Task<JsonResult> GetCartItems()
        {
            var items = new List<CartItemDto>();

            if (Session["LoginStatus"]?.ToString() == "login")
            {
                int userId = int.Parse(Session["UserId"]?.ToString() ?? "0");
                items = await _cartService.GetCartItemsAsync(userId);
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}