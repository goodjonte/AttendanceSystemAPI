namespace AttendanceSystemAPI.Models
{
    public class Absence
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string? StudentsName { get; set; }
        public Guid ClassId { get; set; }
        public string? ClassesName { get; set; }
        public Guid AttendanceId { get; set; }
        public AttendanceStatus Status { get; set; }

    }
}
