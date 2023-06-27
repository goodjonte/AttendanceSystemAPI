namespace AttendanceSystemAPI.Models
{
    public class SchoolClass
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public string? TeachersName { get; set; }
        public string? ClassName { get; set; }
    }
}
