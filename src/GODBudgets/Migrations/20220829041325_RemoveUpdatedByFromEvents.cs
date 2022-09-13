using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GODBudgets.Migrations
{
    public partial class RemoveUpdatedByFromEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "events");

            migrationBuilder.DropColumn(
                name: "updated_by_email",
                table: "events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "updated_by",
                table: "events",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by_email",
                table: "events",
                type: "text",
                nullable: true);
        }
    }
}
