using AttendanceSystemAPI.Models;

namespace AttendanceSystemAPI.DTO
{
    public class AbsenceDTO
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public AttendanceStatus Status { get; set; }
        public Guid AttendanceId { get; set; }
    }
}
