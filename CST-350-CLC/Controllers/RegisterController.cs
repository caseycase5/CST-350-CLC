using CST_350_CLC.Models;
using CST_350_CLC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST_350_CLC.Controllers {
    public class RegisterController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult ProcessRegister(UserModel userModel) {
            SecurityService securityService = new SecurityService();

            if (securityService.ValidRegister(userModel))
                return View("RegisterSuccess", userModel);
            else
                return View("RegisterFailure", userModel);
        }
    }
}
