namespace AttendanceSystemAPI.Models
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
        public Guid ClassesPeriodId { get; set; }
        public bool IsPresent { get; set; }
        public bool IsLate { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public bool UnjustifiedResolved { get; set; }
    }

    public enum AttendanceStatus
    {
        Present = 0,
        Justified = 1,
        Unjustified = 2,
        OverseasJustified = 3
    }
}
