using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class pe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfPeriods",
                table: "SchoolDay",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPeriods",
                table: "SchoolDay");
        }
    }
}
