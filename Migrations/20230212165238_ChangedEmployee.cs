﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeStaff.Migrations
{
    /// <inheritdoc />
    public partial class ChangedEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "FirstName + ' ' + LastName");
        }
    }
}