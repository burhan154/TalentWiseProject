namespace WiseProject.Models.Dto
{
    public class CanEnroll
    {
        //public Enrollment Enrollment { get; set; }
        public int CourseId { get; set; }
        public int CourseCount { get; set; }
        public int EnrollmentCount { get; set; }
        public bool IsEnrolled { get; set; }
    }
}
