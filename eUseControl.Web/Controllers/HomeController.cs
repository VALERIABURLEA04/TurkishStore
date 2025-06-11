using businessLogic.Dtos.ContactDtos;
using businessLogic.Interfaces;
using eUseControlBussinessLogic;
using eUseControlBussinessLogic.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectOnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContact _contactBL;
        private readonly ISession _sessionBL;
        private readonly IProductRepository _productRepositoryBL;

        // Parameterless constructor manually instantiating dependencies
        public HomeController()
        {
            var bl = new BusinesLogic();
            _contactBL = bl.GetContactBL();
            _sessionBL = bl.GetSessionBL();
            _productRepositoryBL = bl.GetProductRepository();
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult NewIn()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(string query)
        {
            var businessProducts = _productRepositoryBL.Search(query); // returns IEnumerable<ProductDataEntities>
            var viewModels = ProductMapper.ToViewModelList(businessProducts);
            return View(viewModels);
        }

        // POST: Home/ContactUs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ContactUs(ContactDto contactData)
        {
            if (ModelState.IsValid)
            {
                var entity = ContactMapper.ToEntity(contactData);  // Map UContactData -> ContactMessageEntity
                await _contactBL.AddAsync(entity);
                TempData["SuccessMessage"] = "Your message has been sent successfully!";
                return RedirectToAction("ContactUs");
            }
            return View(contactData);
        }

    }
}