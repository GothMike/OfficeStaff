using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeStaff.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "PositionHistory",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "PositionHistory");
        }
    }
}
