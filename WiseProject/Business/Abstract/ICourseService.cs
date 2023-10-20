
using WiseProject.Models;
using WiseProject.Data.Results;
using IResult = WiseProject.Data.Results.IResult;
using WiseProject.Models.Dto;

namespace WiseProject.Business.Abstract
{
    public interface ICourseService
    {
        //IDataResult<List<Course>> GetList(int page, int maxRows);
        //IDataResult<List<Course>> GetList(int page, int maxRows, string query);

        IDataResult<List<Course>> GetList(int maxRows);
        IDataResult<List<Course>> GetList();

        IDataResult<Course> Get(int id);
        IResult Add(Course course);
        IResult Update(Course course);
        IResult Delete(int courseId);

        int CountAssignments(int courseId);

        IResult UpdateEnrolmentCount(int courseId, int count);
        IResult IncreaseEnrolmentCount(int courseId);
        IResult DecreaseEnrolmentCount(int courseId);

        IDataResult<SearchCourse> GetSearchList(int page, int maxRows, string query);
    }
}
