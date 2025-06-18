using eUSeControl.BusinessLogic.Services;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController()
        {
            _contactService = ContactService.GetInstance();
        }
    }
}