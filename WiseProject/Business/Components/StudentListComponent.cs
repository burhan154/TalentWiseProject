using Microsoft.AspNetCore.Mvc;
using WiseProject.Business.Abstract;

namespace WiseProject.Business.Components
{
    public class StudentListComponent : ViewComponent
    {
        IEnrollmentService _enrolmentService;
        public StudentListComponent(IEnrollmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }
        public IViewComponentResult Invoke(int courseId)
        {
            return View(_enrolmentService.GetEnrolledStudents(courseId).Data);
        }
    }
}
