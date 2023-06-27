using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoticeCreator",
                table: "Notice");

            migrationBuilder.AddColumn<string>(
                name: "NoticeCreatorName",
                table: "Notice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoticeCreatorName",
                table: "Notice");

            migrationBuilder.AddColumn<Guid>(
                name: "NoticeCreator",
                table: "Notice",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
