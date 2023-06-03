using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystemAPI.Models
{
    public class SchoolWeek
    {
        public Guid Id { get; set; }
        public bool SameEveryDay { get; set; } // if true monday will be every day
        [NotMapped]
        public List<Guid> DailySchedule { get; set; } // List of Guids representing the schedule for each day

    }
}
