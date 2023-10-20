using WiseProject.Data.Results;
using WiseProject.Models;
using WiseProject.Models.Dto;
using IResult = WiseProject.Data.Results.IResult;

namespace WiseProject.Business.Abstract
{
    public interface IEnrollmentService
    {
        //IDataResult<List<Enrollment>> GetListByCourseId(int courseId);

        IDataResult<Enrollment> Get(int id);
        IResult Add(int courseId);
        IResult Update(Enrollment assignment);
        IResult Delete(int assignmentId);

        IDataResult<CanEnroll> EnrollComponent(int courseId);
        IDataResult<EnrolledStudents> GetEnrolledStudents(int courseId);
    }
}
