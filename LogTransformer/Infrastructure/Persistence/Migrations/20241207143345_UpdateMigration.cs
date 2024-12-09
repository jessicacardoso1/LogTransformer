using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LogTransformer.Infrastructure.Persistence.Migrations
{
    public partial class UpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    OriginalContent = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransformedLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Provider = table.Column<string>(nullable: true),
                    HttpMethod = table.Column<string>(nullable: true),
                    StatusCode = table.Column<int>(nullable: false),
                    UriPath = table.Column<string>(nullable: true),
                    TimeTaken = table.Column<int>(nullable: false),
                    ResponseSize = table.Column<int>(nullable: false),
                    CacheStatus = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    OriginalLogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransformedLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransformedLogs_Logs_OriginalLogId",
                        column: x => x.OriginalLogId,
                        principalTable: "Logs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransformedLogs_OriginalLogId",
                table: "TransformedLogs",
                column: "OriginalLogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransformedLogs");

            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
