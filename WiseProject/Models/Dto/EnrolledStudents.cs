namespace WiseProject.Models.Dto
{
    public class EnrolledStudents
    {
        //public Enrollment Enrollment { get; set; }
        public int CourseId { get; set; }
        public int CourseCount { get; set; }
        public int EnrollmentCount { get; set; }
        public List<string> Names{ get; set; }
    }
}
