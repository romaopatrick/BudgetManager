using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GODBudgets.Migrations
{
    public partial class AddWorkingDaysToCompleteAndRemoveExpectedCompletionDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expected_completion_date",
                table: "budgets");

            migrationBuilder.AddColumn<int>(
                name: "working_days_to_complete",
                table: "budgets",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "working_days_to_complete",
                table: "budgets");

            migrationBuilder.AddColumn<DateTime>(
                name: "expected_completion_date",
                table: "budgets",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
