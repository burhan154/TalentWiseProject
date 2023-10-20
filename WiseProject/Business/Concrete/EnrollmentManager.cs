using WiseProject.Business.Abstract;
using WiseProject.Business.Constants;
using WiseProject.DAL.Abstract;
using WiseProject.Data.Results;
using WiseProject.Models;
using WiseProject.Models.Dto;

namespace WiseProject.Business.Concrete
{
    public class EnrollmentManager : IEnrollmentService
    {
        private IEnrollmentDal _enrollmentDal;
        private readonly ICurrentUserService _currentUser;
        private ICourseService _courseService;
        public EnrollmentManager(IEnrollmentDal enrollmentDal, ICurrentUserService currentUser, ICourseService courseService)
        {
            _enrollmentDal = enrollmentDal;
            _currentUser = currentUser;
            _courseService = courseService;
        }

        public bool IsEnrolled(int courseId)
        {
            var userId = _currentUser.UserId();
            var enroll = _enrollmentDal.Get(x => x.UserId == userId && x.CourseId == courseId);
            if (enroll != null)
            {
                return true;
            }
            return false;
        }

        public Data.Results.IResult Add(int courseId)
        {
            if(IsEnrolled(courseId))
            {
                return new ErrorResult();
            }
            if (_currentUser.UserId() == "")
            {
                return new ErrorResult();
            }
            var enrollment = new Enrollment()
            {
                UserId = _currentUser.UserId(),
                EnrollmentDate = DateTime.Now,
                CourseId = courseId
            };
            _enrollmentDal.Add(enrollment);
            _courseService.IncreaseEnrolmentCount(courseId);
            return new SuccessResult();
        }

        public Data.Results.IResult Delete(int enrollmentId)
        {
            var enroll = _enrollmentDal.Get(x => x.Id == enrollmentId);
            var courseId = enroll.CourseId;
            _enrollmentDal.Delete(enroll);
            _courseService.DecreaseEnrolmentCount(courseId);
            return new SuccessResult();
        }

        public IDataResult<Enrollment> Get(int id)
        {
            var enroll = _enrollmentDal.Get(x => x.Id == id);
            return new SuccessDataResult<Enrollment>(enroll, Messages.ItemsListed);
        }

        public Data.Results.IResult Update(Enrollment enrollment)
        {
            _enrollmentDal.Update(enrollment);
            return new SuccessResult();
        }

        public IDataResult<EnrolledStudents> GetEnrolledStudents(int courseId)
        {
            var students = _enrollmentDal.GetList(x => x.CourseId == courseId);
            var course = _courseService.Get(courseId).Data;
            List<string> names = new List<string>();
            foreach (var student in students)
            {
                var user = _currentUser.GetUserById(student.UserId);
                names.Add(user.FirstName+ " "+user.LastName);
            }

            var enroll = new EnrolledStudents()
            {
                CourseId = courseId,
                EnrollmentCount = course.EnrollmentCount,
                CourseCount = _courseService.CountAssignments(courseId),
                Names = names
            };

            
            return new SuccessDataResult<EnrolledStudents>(enroll, Messages.ItemsListed);
            //var course = _courseService.Get(courseId).Data;
        }


        public IDataResult<CanEnroll> EnrollComponent(int courseId)
        {
            var course = _courseService.Get(courseId).Data;
            var enroll = new CanEnroll()
            {
                CourseId= courseId,
                EnrollmentCount = course.EnrollmentCount,
                IsEnrolled = IsEnrolled(courseId),
                CourseCount = _courseService.CountAssignments(courseId),
            };
            return new SuccessDataResult<CanEnroll>(enroll, Messages.ItemsListed);
            //var course = _courseService.Get(courseId).Data;
        }
    }
}
