using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class FlowTaskValueTablesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainStateId",
                table: "Task",
                newName: "StateId");

            migrationBuilder.RenameColumn(
                name: "MainState",
                table: "State",
                newName: "FlowId");

            migrationBuilder.AddColumn<int>(
                name: "FlowId",
                table: "Task",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlowId",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Task",
                newName: "MainStateId");

            migrationBuilder.RenameColumn(
                name: "FlowId",
                table: "State",
                newName: "MainState");
        }
    }
}
