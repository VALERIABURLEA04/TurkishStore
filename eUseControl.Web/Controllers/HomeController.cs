using businessLogic.Dtos.BlogDtos;
using businessLogic.Dtos.ProductDtos;
using businessLogic.Interfaces.Repositories;
using eUseControlBussinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectOnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IProductRepository _productRepository;

        public HomeController()
        {
            var bl = new BusinesLogic();

            _productRepository = bl.GetProductRepository();
            _blogPostRepository = bl.GetBlogPostRepository();
        }

        // GET: /Home/Index
        public async Task<ActionResult> Index()
        {
            int userId = int.Parse(Session["UserId"]?.ToString() ?? "0");
            List<ProductDto> products = await _productRepository.GetAllProducts(userId);

            return View(products);
        }

        // GET: /Home/Blog
        public async Task<ActionResult> Blog()
        {
            List<BlogPostDto> blogPosts = await _blogPostRepository.GetBlogPostsAsync();
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