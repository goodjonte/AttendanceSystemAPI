using System.Text.Json.Nodes;

namespace AttendanceSystemAPI.DTO
{
    public class TimeTableDTO
    {
        public bool SameEveryDay { get; set; }
        public int MostPeriods { get; set; }
        public Guid MondayId { get; set; }
        public List<PeriodDTO> MondayColumn { get; set; }
        public Guid TuesdayId { get; set; }
        public List<PeriodDTO> TuesdayColumn { get; set; }
        public Guid WednesdayId { get; set; }
        public List<PeriodDTO> WednesdayColumn { get; set; }
        public Guid ThursdayId { get; set; }
        public List<PeriodDTO> ThursdayColumn { get; set; }
        public Guid FridayId { get; set; }
        public List<PeriodDTO> FridayColumn { get; set; }
    }
}
