using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchMe.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Bars_MyBarId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MyBarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MyBarId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bars_UserId",
                table: "Bars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bars_Users_UserId",
                table: "Bars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bars_Users_UserId",
                table: "Bars");

            migrationBuilder.DropIndex(
                name: "IX_Bars_UserId",
                table: "Bars");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bars");

            migrationBuilder.AddColumn<int>(
                name: "MyBarId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MyBarId",
                table: "Users",
                column: "MyBarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Bars_MyBarId",
                table: "Users",
                column: "MyBarId",
                principalTable: "Bars",
                principalColumn: "Id");
        }
    }
}
