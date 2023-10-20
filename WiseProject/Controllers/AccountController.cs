using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WiseProject.Data;
using WiseProject.Areas.Identity.Pages.Account;
using WiseProject.Models;
using WiseProject.Business.Concrete;

namespace WiseProject.Controllers
{
    public class AccountController : Controller
    {

        ServiceManager _serviceManager;
        public AccountController(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {   
            if (ModelState.IsValid)
            {
                await _serviceManager.Register(model);
            }
            return RedirectToAction("Index", "Home");   
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
