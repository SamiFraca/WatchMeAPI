using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchMe.Migrations
{
    /// <inheritdoc />
    public partial class UserIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bars_Users_UserId",
                table: "Bars");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bars_Users_UserId",
                table: "Bars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bars_Users_UserId",
                table: "Bars");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Bars_Users_UserId",
                table: "Bars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
