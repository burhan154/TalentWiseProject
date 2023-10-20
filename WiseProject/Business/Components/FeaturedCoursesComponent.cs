using Microsoft.AspNetCore.Mvc;
using WiseProject.Business.Abstract;

namespace WiseProject.Business.Components
{
    public class FeaturedCoursesComponent:ViewComponent
    {
        ICourseService _courseService;
        public FeaturedCoursesComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public IViewComponentResult Invoke()
        {
            int max = 3;
            return View(_courseService.GetList(max).Data);
        }
    }
}
