using CST_350_CLC.Models;
using CST_350_CLC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CST_350_CLC.Controllers {
    public class LoginController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult ProcessLogin(UserModel userModel) {
            SecurityService securityService = new SecurityService();

            if (securityService.IsValid(userModel))
                return View("LoginSuccess", userModel);
            else
                return View("LoginFailure", userModel);
        }
    }
}
