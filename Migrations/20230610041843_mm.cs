using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class mm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "DailyScheduleJsonArrayString",
                table: "SchoolWeek",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DaysPeriodsJsonArrayString",
                table: "SchoolDay",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyScheduleJsonArrayString",
                table: "SchoolWeek");

            migrationBuilder.DropColumn(
                name: "DaysPeriodsJsonArrayString",
                table: "SchoolDay");

            
        }
    }
}
