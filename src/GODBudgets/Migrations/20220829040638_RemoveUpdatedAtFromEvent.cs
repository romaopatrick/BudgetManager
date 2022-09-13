using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GODBudgets.Migrations
{
    public partial class RemoveUpdatedAtFromEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "events");

            migrationBuilder.AlterColumn<string>(
                name: "snapshot",
                table: "events",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "snapshot",
                table: "events",
                type: "jsonb",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "events",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
