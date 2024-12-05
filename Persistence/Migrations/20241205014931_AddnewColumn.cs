using Microsoft.EntityFrameworkCore.Migrations;

namespace LogTransformer.Persistence.Migrations
{
    public partial class AddnewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "TransformedLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "TransformedLogs");
        }
    }
}
