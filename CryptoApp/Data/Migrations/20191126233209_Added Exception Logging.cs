using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CryptoApp.Data.Migrations
{
    public partial class AddedExceptionLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exceptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExceptionMessage = table.Column<string>(nullable: true),
                    ExceptionStackTrace = table.Column<string>(nullable: true),
                    HttpStatusCode = table.Column<int>(nullable: false),
                    LogTime = table.Column<DateTime>(nullable: false),
                    RequestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exceptions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exceptions");
        }
    }
}
