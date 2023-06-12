namespace AttendanceSystemAPI.Models
{
    public class ClassesPeriods
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public Guid PeriodId { get; set; }
        public Guid DayId { get; set; }
    }
}
