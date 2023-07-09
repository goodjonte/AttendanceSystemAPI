using AttendanceSystemAPI.Models;

namespace AttendanceSystemAPI.DTO
{
    public class AttendanceDTO
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public string? ClassesPeriod { get; set; }
        public bool IsPresent { get; set; }
        public bool IsLate { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}
