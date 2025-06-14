﻿using eUseControl.Domain.Entities.CartEntities;
using eUseControl.Domain.Entities.Listings;
using eUseControlBussinessLogic;
using System.Web.Mvc;

namespace ProjectOnlineStore.Controllers
{
    [Authorize] // Ensure only logged-in users with valid cookie access cart functions
    public class CartController : Controller
    {
        private Cart GetCart()
        {
            var bl = new BusinesLogic();
            var session = bl.GetSessionBL();
            var user = session.GetUserByCookie(Request.Cookies["X-KEY"]?.Value);

            if (user == null)
            {
                return null;
            }

            // Using UserId instead of Id
            string cartKey = $"Cart_{user.UserId}";
            Cart cart = Session[cartKey] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session[cartKey] = cart;
            }
            return cart;
        }

        // Added ShoppingCart action to fix 404 for /Cart/ShoppingCart URL
        public ActionResult ShoppingCart()
        {
            var cart = GetCart();
            if (cart == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(cart);
        }

        // Also keep Index as alternative entry point
        public ActionResult Index()
        {
            return RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        public ActionResult AddToCart(Item item)
        {
            if (ModelState.IsValid)
            {
                var cart = GetCart();
                if (cart == null)
                {
                    return RedirectToAction("Login", "Auth");
                }
                cart.AddItem(item);
                return RedirectToAction("ShoppingCart");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int clothId)
        {
            var cart = GetCart();
            if (cart == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            cart.RemoveItem(clothId);
            return RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int clothId, int quantity)
        {
            var cart = GetCart();
            if (cart == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            cart.UpdateQuantity(clothId, quantity);
            return RedirectToAction("ShoppingCart");
        }

        public ActionResult ClearCart()
        {
            var cart = GetCart();
            if (cart == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            cart.Clear();
            return RedirectToAction("ShoppingCart");
        }
    }
}