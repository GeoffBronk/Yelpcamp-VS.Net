using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yelpcamp.Migrations
{
    public partial class CleanupChildData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CampgroundsId",
                table: "CampgroundReviews");

            migrationBuilder.DropColumn(
                name: "CampgroundsId",
                table: "CampgroundImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampgroundsId",
                table: "CampgroundReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CampgroundsId",
                table: "CampgroundImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
