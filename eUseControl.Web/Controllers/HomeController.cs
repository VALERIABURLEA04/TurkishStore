using businessLogic.Services;
using eUSeControl.BusinessLogic.Dtos.BlogDtos;
using eUSeControl.BusinessLogic.Dtos.ProductDtos;
using eUSeControl.BusinessLogic.Interfaces;
using eUSeControl.BusinessLogic.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectOnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IProductService _productService;

        public HomeController()
        {
            _productService = ProductService.GetInstance();
            _blogPostService = BlogPostService.GetInstance();
        }

        // GET: /Home/Index
        public async Task<ActionResult> Index()
        {
            int userId = int.Parse(Session["UserId"]?.ToString() ?? "0");
            List<ProductDto> products = await _productService.GetAllProductsAsync(userId);

            return View(products);
        }

        // GET: /Home/Blog
        public async Task<ActionResult> Blog()
        {
            List<BlogPostDto> blogPosts = await _blogPostService.GetBlogPostsAsync();
            return View(blogPosts);
        }

        // GET: /Home/About
        public ActionResult About()
        {
            return View();
        }

        // GET: /Home/Contact
        public ActionResult Contact()
        {
            return View();
        }

        // GET /Home/Features
        public ActionResult Features()
        {
            return View();
        }
    }
}