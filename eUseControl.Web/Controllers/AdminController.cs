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
            var userId = int.Parse(Session["UserId"]?.ToString() ?? "0");
            var users = await _userService.GetAllUsersAsync();
            var result = users.Where(x => x.Id != userId).ToList();

            return View(result);
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
            var products = await _productService.GetAllProductsAsync(0);
            return View(products);
        }

        // POST: /Admin/DeleteProduct
        public ActionResult DeleteProduct(int id)
        {
            _productService.DeleteProductAsync(id);
            return Json(new { success = true });
        }
    }
}