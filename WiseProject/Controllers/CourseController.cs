using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WiseProject.Business.Abstract;
using WiseProject.Models;

namespace WiseProject.Controllers
{
    //[Authorize(Roles = "Admin,Instructor")]
    //[ValidateAntiForgeryToken]
    public class CourseController : Controller
    {
        ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        //[Route("Course/{id?}{query?}")]
        [Route("Course")]
        public IActionResult Index(int courseId = 1, string query="")
        {
            int max = 6;
            return View(_courseService.GetSearchList(courseId, max,query).Data);
        }

        public IActionResult Details(int id)
        {
            return View(_courseService.Get(id).Data);
        }
            
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Update()
        {
            return View();
        }
        
        
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Delete(int id)
        {
            return View(_courseService.Get(id).Data);
        }
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Edit(int id)
        {
            return View(_courseService.Get(id).Data);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Edit(Course course)
        {
            var result = _courseService.Update(course);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Create(Course course)
        {
            var result = _courseService.Add(course);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Instructor")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _courseService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
