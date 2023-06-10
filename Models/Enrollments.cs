using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystemAPI.Models
{
    public class Enrollments
    {
        [Key]
        public Guid EnrollmentId {  get; set; }
        public Guid? StudentId { get; set; }
        public Guid? ClassId { get; set; }
        public string? StudentName { get; set; }
    }
}
