using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenseServer.Database.Migrations
{
    public partial class AddLicenseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LicenseId",
                table: "Events",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_LicenseId",
                table: "Events",
                column: "LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Licenses_LicenseId",
                table: "Events",
                column: "LicenseId",
                principalTable: "Licenses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Licenses_LicenseId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_LicenseId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "Events");
        }
    }
}
