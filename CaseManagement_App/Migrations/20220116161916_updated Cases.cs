using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseManagement_App.Migrations
{
    public partial class updatedCases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ContactInfoId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Header",
                table: "Cases",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactInfoId",
                table: "Users",
                column: "ContactInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ContactInfoId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "ContactInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Header",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactInfoId",
                table: "Users",
                column: "ContactInfoId",
                unique: true);
        }
    }
}
