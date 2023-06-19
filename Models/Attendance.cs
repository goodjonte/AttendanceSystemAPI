namespace AttendanceSystemAPI.Models
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
        public bool IsPresent { get; set; }
        public bool IsLate { get; set; }
        public DateTime Date { get; set; }
    }
}
