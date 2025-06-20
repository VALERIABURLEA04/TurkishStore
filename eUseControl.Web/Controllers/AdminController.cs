using businessLogic.Dtos.UserDtos;
using eUSeControl.BusinessLogic.Dtos.ProductDtos;
using eUSeControl.BusinessLogic.Interfaces;
using eUSeControl.BusinessLogic.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public AdminController()
        {
            _userService = UserService.GetInstance();
            _productService = ProductService.GetInstance();
        }

        // GET: /Admin/Users
        public async Task<ActionResult> Users()
        {
            if (Session["LoginStatus"]?.ToString() != "login")
                return RedirectToAction("Login", "Auth");

            var userId = int.Parse(Session["UserId"]?.ToString() ?? "0");
            var users = await _userService.GetAllUsersAsync();
            var result = users.Where(x => x.Id != userId).ToList();

            return View(result);
        }

        // GET: /Admin/CreateUser
        public ActionResult CreateUser()
        {
            if (Session["LoginStatus"]?.ToString() != "login")
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // POST: /Admin/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser(UpsertUserDto model)
        {
            var result = await _userService.AddUserAsync(model);

            if (!result)
                return View();

            return RedirectToAction("Users");
        }

        // GET: /Admin/UpdateUser
        public async Task<ActionResult> UpdateUser(int id)
        {
            if (Session["LoginStatus"]?.ToString() != "login")
                return RedirectToAction("Login", "Auth");

            var user = await _userService.GetUserByIdAsync(id);
            return View(user);
        }

        // POST: /Admin/UpdateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateUser(UpsertUserDto model)
        {
            var result = await _userService.UpdateUserAsync(model);

            if (!result)
                return View();

            return RedirectToAction("Users");
        }

        // POST: /Admin/DeleteUser
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUserById(id);
            return Json(new { success = true });
        }

        // GET: /Admin/Products
        public async Task<ActionResult> Products()
        {
            if (Session["LoginStatus"]?.ToString() != "login")
                return RedirectToAction("Login", "Auth");

            var products = await _productService.GetAllProductsAsync(0);
            return View(products);
        }

        // GET /Admin/CreateProduct
        public ActionResult CreateProduct()
        {
            if (Session["LoginStatus"]?.ToString() != "login")
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // POST /Admin/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProduct(UpsertProductDto model)
        {
            var result = await _productService.AddProductAsync(model);

            if (!result)
                return View();

            return RedirectToAction("Products");
        }

        // GET /Admin/UpdateProduct
        public async Task<ActionResult> UpdateProduct(int id)
        {
            if (Session["LoginStatus"]?.ToString() != "login")
                return RedirectToAction("Login", "Auth");

            var product = await _productService.GetProductByIdAsync(id);
            return View(product);
        }

        // POST /Admin/UpdateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProduct(UpsertProductDto model)
        {
            var result = await _productService.UpdateProductAsync(model);

            if (!result)
                return View();

            return RedirectToAction("Products");
        }

        // POST: /Admin/DeleteProduct
        public ActionResult DeleteProduct(int id)
        {
            _productService.DeleteProductAsync(id);
            return Json(new { success = true });
        }
    }
}