using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenseServer.Database.Migrations
{
    public partial class MakeFileNamesUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductFiles_Name",
                table: "ProductFiles",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductFiles_Name",
                table: "ProductFiles");
        }
    }
}
