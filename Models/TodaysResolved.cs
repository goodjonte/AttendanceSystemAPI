namespace AttendanceSystemAPI.Models
{
    public class TodaysResolved
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public DateTime DateValid { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}
