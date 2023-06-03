using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystemAPI.Models
{
    public class SchoolDay
    {
        public Guid Id { get; set; }
        public DayOfWeek day { get; set; }
        [NotMapped]
        public List<Guid> DaysPeriods { get; set; }

    }

    public enum DayOfWeek
    {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4
    }
}
