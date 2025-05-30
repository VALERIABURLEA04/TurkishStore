using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using businessLogic.BLStruct;
using businessLogic.DBModel;
using businessLogic.Interfaces;
using eUseControl.Domain.Entities.Listings;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities.User.UserActionResponse;
using eUseControlBussinessLogic.Interfaces;


namespace eUseControl.Web.Controllers
{
    public class ListingsController : Controller
    {
        private readonly IUser _userBL;
        private readonly IListings _listingsBL;

        // Constructor injection of business logic layers
        public ListingsController(IUser userBL, IListings listingsBL)
        {
            _userBL = userBL;
            _listingsBL = listingsBL;
        }

        // GET: Listings
        public async Task<ActionResult> Index()
        {
            // Get current user ID (from cookie)
            int currentUserId = -1;
            if (Request.Cookies["X-KEY"] != null)
            {
                var xKey = Request.Cookies["X-KEY"].Value;
                // var username = eUseControl.Helpers.Session.CookieGenerator.Validate(xKey);
                var username = "Daniela Postolachi"; // For testing

                // Use IUser BL instead of raw DbContext
                var user = await _userBL.GetUserByUsernameOrEmailAsync(username);
                if (user != null)
                    currentUserId = user.Id;
            }
            ViewBag.CurrentUserId = currentUserId;

            // Get listings via IListing interface
            var listings = await _listingsBL.GetAllListingsAsync();

            // **NOTE:** listings should be a collection of your domain model (e.g. Item or Listing)
            return View(listings.OrderByDescending(l => l.AddedDate).ToList());
        }

        // GET: Listings/Create
        public ActionResult Create()
        {
            if (Request.Cookies["X-KEY"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        // POST: Listings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateListingViewModel model)
        {
            if (Request.Cookies["X-KEY"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                var xKey = Request.Cookies["X-KEY"].Value;
                // var username = eUseControl.Helpers.CookieGenerator.Validate(xKey);
                var username = "Daniela Postolachi"; // For testing

                var user = await _userBL.GetUserByUsernameOrEmailAsync(username);
                if (user == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                // **Adapt this to your domain:** Here I use your CreateListingViewModel and create a new Item or Listing entity
                var listing = new Item // or Listing, if you have Listing entity
                {
                    // Match properties - 
                    // You'll need to add or map 'Title', 'Description' etc. if they exist on Item or Listing
                    ClothName = model.Title,               // CHANGE: model.Title => Item property ClothName (example)
                    // Assuming Description maps to some other property on Item, you might need to add it or ignore
                    Price = model.Price,                   // same
                    ImageUrl = model.ImageUrl,
                    AddedDate = DateTime.UtcNow,          // map CreatedAt to AddedDate if needed
                    // UserId? You might need to add UserId property to Item or handle separately
                };


                await _listingsBL.CreateListingAsync(model);


                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Listings/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var listing = await _listingsBL.GetListingByIdAsync(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        // GET: Listings/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (Request.Cookies["X-KEY"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var listing = await _listingsBL.GetListingByIdAsync(id);
            if (listing == null)
            {
                return HttpNotFound();
            }

            var user = await GetCurrentUserAsync();
            if (listing.UserId != user.Id && !user.IsAdmin)
            {
                return new HttpStatusCodeResult(403); // Forbidden
            }

            // Map Item or Listing back to CreateListingViewModel for editing
            var model = new CreateListingViewModel
            {
                Title = listing.ClothName,        // CHANGE: adapt property names accordingly
                // Description: if you have it, map it here, otherwise ignore or add
                Price = listing.Price,
                ImageUrl = listing.ImageUrl
            };
            return View(model);
        }

        // POST: Listings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreateListingViewModel model)
        {
            if (Request.Cookies["X-KEY"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var listing = await _listingsBL.GetListingByIdAsync(id);
            if (listing == null)
            {
                return HttpNotFound();
            }

            var user = await GetCurrentUserAsync();
            if (listing.UserId != user.Id && !user.IsAdmin)
            {
                return new HttpStatusCodeResult(403);
            }

            if (ModelState.IsValid)
            {
                // Update listing properties from model
                listing.ClothName = model.Title;        // CHANGE: adapt as needed
                listing.Price = model.Price;
                listing.ImageUrl = model.ImageUrl;
                // Update Description if applicable

                await _listingsBL.UpdateListingAsync(id, model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Listings/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (Request.Cookies["X-KEY"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var listing = await _listingsBL.GetListingByIdAsync(id);
            if (listing == null)
            {
                return HttpNotFound();
            }

            var user = await GetCurrentUserAsync();
            if (listing.UserId != user.Id && !user.IsAdmin)
            {
                return new HttpStatusCodeResult(403);
            }

            return View(listing);
        }

        // POST: Listings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Request.Cookies["X-KEY"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var listing = await _listingsBL.GetListingByIdAsync(id);
            if (listing == null)
            {
                return HttpNotFound();
            }

            var user = await GetCurrentUserAsync();
            if (listing.UserId != user.Id && !user.IsAdmin)
            {
                return new HttpStatusCodeResult(403);
            }

            await _listingsBL.DeleteListingAsync(id);
            return RedirectToAction("Index");
        }

        // Helper method to get current user info from cookie and IUser BL
        private async Task<(int Id, bool IsAdmin)> GetCurrentUserAsync()
        {
            int currentUserId = -1;
            bool isAdmin = false;
            if (Request.Cookies["X-KEY"] != null)
            {
                var xKey = Request.Cookies["X-KEY"].Value;
                // var username = eUseControl.Helpers.Session.CookieGenerator.Validate(xKey);
                var username = "Daniela Postolachi"; // For testing

                var user = await _userBL.GetUserByUsernameOrEmailAsync(username);
                if (user != null)
                {
                    currentUserId = user.Id;
                    // Example: user.Level is enum or int, adjust as needed
                    isAdmin = user.Level.ToString().ToLower() == "admin" || (int)user.Level == 1;
                }
            }
            return (currentUserId, isAdmin);
        }
    }
}