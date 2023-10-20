using WiseProject.Business.Abstract;
using WiseProject.Business.Constants;
using WiseProject.DAL.Abstract;
using WiseProject.Models;
using WiseProject.Data.Results;
using IResult = WiseProject.Data.Results.IResult;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Web;
using WiseProject.Models.Dto;

namespace WiseProject.Business.Concrete
{
    public class CourseManager : ICourseService
    {
        private ICourseDal _courseDal;
        private readonly ICurrentUserService _currentUser;
        private IAssignmentService _assignmentService;
        //private IEnrollmentService _enrollmentService;


        public CourseManager(ICourseDal courseDal, ICurrentUserService currentUser, IAssignmentService assignmentService)
        {
            _courseDal = courseDal;
            _currentUser = currentUser;
            _assignmentService = assignmentService;

            //_enrollmentService = enrollmentService;
        }

        public IResult Add(Course course)
        {
            course.UserId = _currentUser.UserId();
            _courseDal.Add(course);
            return new SuccessResult();
        }

        public IResult Delete(int courseId)
        {
            var course = _courseDal.Get(x => x.Id == courseId);
            
            _courseDal.Delete(course);
            return new SuccessResult();
        }

        public IDataResult<Course> Get(int id)
        {
            var course = _courseDal.Get(x => x.Id == id);
            course.Assignment = _assignmentService.GetListByCourseId(course.Id).Data;
            course.User = _currentUser.GetUserById(course.UserId);
            return new SuccessDataResult<Course>(course, Messages.ItemsListed);
        }

        public IDataResult<List<Course>> GetList(int maxRows)
        {
            var courses = _courseDal.GetList().Take(maxRows).ToList();
            foreach (var course in courses)
            {
                course.User = _currentUser.GetUserById(course.UserId);
            }
            if (courses.Count() == 0)
                return new ErrorDataResult<List<Course>>(Messages.ItemNotFound);

            return new SuccessDataResult<List<Course>>(courses, Messages.ItemsListed);
        }

        public IDataResult<List<Course>> GetList()
        {
            var courses = _courseDal.GetList().ToList();
            foreach (var course in courses)
            {
                course.User = _currentUser.GetUserById(course.UserId);
            }
            if (courses.Count() == 0)
                return new ErrorDataResult<List<Course>>(Messages.ItemNotFound);

            return new SuccessDataResult<List<Course>>(courses, Messages.ItemsListed);
        }

        /*public IDataResult<List<Course>> GetList(int page, int maxRows)
        {
            var courses = _courseDal.GetList().Skip((page - 1) * maxRows).Take(maxRows).ToList();
            if (courses.Count() == 0)
                return new ErrorDataResult<List<Course>>(Messages.ItemNotFound);

            return new SuccessDataResult<List<Course>>(courses, Messages.ItemsListed);
        }*/

        /*public IDataResult<List<Course>> GetList(int page, int maxRows, string query)
        {
            var courses = _courseDal.GetList(x => x.Title.Contains(query)).Skip((page - 1) * maxRows).Take(maxRows).ToList();
            if (courses.Count() == 0)
                return new ErrorDataResult<List<Course>>(new List<Course>(), Messages.ItemNotFound);

            foreach (var course in courses)
            {
                course.User = _currentUser.GetUserById(course.UserId);
            }
            return new SuccessDataResult<List<Course>>(courses, Messages.ItemsListed);
        }*/


        public IDataResult<SearchCourse> GetSearchList(int page, int maxRows, string query)
        {

            //var b = _currentUser.Roles();
            List<Course> courses;
            int count;
            if (query == null||query=="")
            {
                courses = _courseDal.GetList().Skip((page - 1) * maxRows).Take(maxRows).ToList();
                count = _courseDal.GetList().Count();
            }
            else
            {
                courses = _courseDal.GetList(x => x.Title.Contains(query)).Skip((page - 1) * maxRows).Take(maxRows).ToList();
                count = _courseDal.GetList(x => x.Title.Contains(query)).Count();
            }
            foreach (var course in courses)
            {
                course.User = _currentUser.GetUserById(course.UserId);
            }

            var search = new SearchCourse()
            {
                Course = courses,
                CurrentPageIndex= page,
                PageCount= count/maxRows+1,
                ResultCount = count,
                Query=query
            };

            if (courses.Count() == 0)
                return new ErrorDataResult<SearchCourse>(search,Messages.ItemNotFound);

            return new SuccessDataResult<SearchCourse>(search, Messages.ItemsListed);
        }

        public IResult UpdateEnrolmentCount(int courseId,int count)
        {
            var course = Get(courseId).Data;
            course.EnrollmentCount = count;
            Update(course);
            return new SuccessResult();
        }

        public IResult IncreaseEnrolmentCount(int courseId)
        {
            var course = Get(courseId).Data;
            course.EnrollmentCount = course.EnrollmentCount +1;
            Update(course);
            return new SuccessResult();
        }

        public IResult DecreaseEnrolmentCount(int courseId)
        {
            var course = Get(courseId).Data;
            course.EnrollmentCount = course.EnrollmentCount - 1;
            Update(course);
            return new SuccessResult();
        }

        public IResult Update(Course course)
        {
            //course.UserId = _currentUser.UserId();
            _courseDal.Update(course);
            return new SuccessResult();
        }

        public int CountAssignments(int courseId)
        {
            return _assignmentService.Count(courseId);
        }
    }
}
