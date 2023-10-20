namespace WiseProject.Models.Dto
{
    public class SearchCourse
    {
        public List<Course> Course { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
        public int ResultCount { get; set; }
        public string Query { get; set; }
    }
}
