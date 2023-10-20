using Microsoft.AspNetCore.Mvc;
using WiseProject.Business.Abstract;

namespace WiseProject.Business.Components
{
    public class CourseSearchComponent:ViewComponent
    {
        ICourseService _courseService;
        public CourseSearchComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public IViewComponentResult Invoke(int courseId = 1, string query = "")
        {
            int max = 6;
            return View(_courseService.GetSearchList(courseId, max, query).Data);
        }
    }
}
