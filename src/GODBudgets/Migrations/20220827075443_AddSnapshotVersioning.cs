using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GODBudgets.Migrations
{
    public partial class AddSnapshotVersioning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "version",
                table: "budgets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "version",
                table: "budgets");
        }
    }
}
