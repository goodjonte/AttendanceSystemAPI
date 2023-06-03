using System.Xml.Linq;

namespace AttendanceSystemAPI.Models
{
    public class SchoolPeriod
    {
        public Guid Id { get; set; }
        public bool IsABreak { get; set; }
        public int PeriodNumber { get; set; }
        public string Name { get; set; }
        public DateTime PeriodStartTime { get; set; }
        public DateTime PeriodEndTime { get; set; }
    }
}
