using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseProject.Business.Abstract;
using WiseProject.Models;

namespace WiseProject.Controllers
{
    public class EnrollmentController : Controller
    {

        IEnrollmentService _enrollmentService;
        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Delete(int id)
        {
            return View(_enrollmentService.Get(id).Data);
        }

        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Edit(int id)
        {
            return View(_enrollmentService.Get(id).Data);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult EnrollCourse(int id)
        {
            var result = _enrollmentService.Add(id);
            return RedirectToAction("Details", "Course", new { id });
        }
    }
}
