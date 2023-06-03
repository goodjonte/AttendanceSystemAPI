using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AttendanceSystemAPI.Models;

namespace AttendanceSystemAPI.Data
{
    public class AttendanceSystemAPIContext : DbContext
    {
        public AttendanceSystemAPIContext (DbContextOptions<AttendanceSystemAPIContext> options)
            : base(options)
        {
        }

        public DbSet<AttendanceSystemAPI.Models.User> User { get; set; } = default!;

        public DbSet<AttendanceSystemAPI.Models.School> School { get; set; } = default!;

        public DbSet<AttendanceSystemAPI.Models.Notice> Notice { get; set; } = default!;

        public DbSet<AttendanceSystemAPI.Models.SchoolWeek> SchoolWeek { get; set; } = default!;

        public DbSet<AttendanceSystemAPI.Models.SchoolDay> SchoolDay { get; set; } = default!;

        public DbSet<AttendanceSystemAPI.Models.SchoolPeriod> SchoolPeriod { get; set; } = default!;

        public DbSet<AttendanceSystemAPI.Models.Enrollments> Enrollments { get; set; } = default!;

        public DbSet<AttendanceSystemAPI.Models.SchoolClass> SchoolClass { get; set; } = default!;
    }
}
