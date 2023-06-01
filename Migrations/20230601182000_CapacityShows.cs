using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchMe.Migrations
{
    /// <inheritdoc />
    public partial class CapacityShows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "actualCap",
                table: "Shows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "maxCap",
                table: "Shows",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "actualCap",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "maxCap",
                table: "Shows");
        }
    }
}
