namespace AttendanceSystemAPI.Models
{
    public class Absence
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
        public Guid AttendanceId { get; set; }

    }
}
