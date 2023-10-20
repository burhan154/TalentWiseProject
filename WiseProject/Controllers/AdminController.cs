using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseProject.Business.Abstract;

namespace Elev8FinalProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IAdminService _adminService;
        private ICourseService _courseService;
        public AdminController(IAdminService adminService,ICourseService courseService)
        {
            _adminService = adminService;
            _courseService = courseService;
        }

        public IActionResult Index()
        {

            return View(_courseService.GetList().Data);
        }

        public IActionResult Instructors()
        {
            return View(_adminService.GetInstructors().Data);
        }

        public IActionResult Users()
        {
            return View(_adminService.GetUsers().Data);
        }
        
        public IActionResult Admins()
        {
            return View(_adminService.GetAdmins().Data);
        }

        public IActionResult Roles()
        {
            return View(_adminService.GetRoles().Data);
        }

        [HttpPost]
        public IActionResult CreateRole(string role)
        {
            _adminService.AddRole(role);
            return RedirectToAction(nameof(Roles));
        }

        [HttpPost]
        public IActionResult DeleteRole(string id)
        {
            _adminService.RemoveRole(id);
            return RedirectToAction(nameof(Roles));
        }

        [HttpPost]
        public IActionResult ChangeRole(string id,string role)
        {
            _adminService.ChangeRole(id,role);
            return RedirectToAction(nameof(Users));
        }
    }
}
