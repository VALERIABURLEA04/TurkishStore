using System.Web.Mvc;
using businessLogic.Interfaces;
using eUseControlBussinessLogic;
using eUseControl.Domain.Entities.User;

namespace ProjectOnlineStore.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _userBL;

        public UserController()
        {
            var bl = new BusinesLogic();
            _userBL = bl.GetUserBL();
        }

        public ActionResult UserList()
        {
            if (Session["AdminUsername"] == null)
                return RedirectToAction("AdminLogin", "Admin");

            var users = _userBL.GetAllUsers();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id)
        {
            var result = _userBL.DeleteUser(id);

            if (!result)
            {
                return HttpNotFound();
            }

            TempData["SuccessMessage"] = "User deleted successfully.";
            return RedirectToAction("UserList");
        }
    }
}