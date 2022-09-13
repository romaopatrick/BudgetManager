using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GODBudgets.Migrations
{
    public partial class AddSnapshotNumberIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_budgets_snapshot_number",
                table: "budgets",
                column: "snapshot_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_budgets_snapshot_number",
                table: "budgets");
        }
    }
}
