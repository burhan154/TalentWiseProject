using Microsoft.AspNetCore.Mvc;
using WiseProject.Business.Abstract;

namespace WiseProject.Business.Components
{
    public class EnrollCourseComponent : ViewComponent
    {
        IEnrollmentService _enrolmentService;
        public EnrollCourseComponent(IEnrollmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }
        public IViewComponentResult Invoke(int courseId)
        {
            return View(_enrolmentService.EnrollComponent(courseId).Data);
        }
    }
}