using System.Text.Json.Nodes;

namespace AttendanceSystemAPI.DTO
{
    public class TimeTableDTO
    {
        public bool SameEveryDay { get; set; }
        public int MostPeriods { get; set; }
        public Guid MondayId { get; set; }
        public List<PeriodDTO> MondayColumn { get; set; } = new List<PeriodDTO>();
        public Guid TuesdayId { get; set; }
        public List<PeriodDTO> TuesdayColumn { get; set; } = new List<PeriodDTO>();
        public Guid WednesdayId { get; set; }
        public List<PeriodDTO> WednesdayColumn { get; set; } = new List<PeriodDTO>();
        public Guid ThursdayId { get; set; }
        public List<PeriodDTO> ThursdayColumn { get; set; } = new List<PeriodDTO>();
        public Guid FridayId { get; set; }
        public List<PeriodDTO> FridayColumn { get; set; } = new List<PeriodDTO>();
    }
}
