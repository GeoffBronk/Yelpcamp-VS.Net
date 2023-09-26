using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yelpcamp.Migrations
{
    public partial class AddCampgroundIdToImageAndReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampgroundImages_Campgrounds_CampgroundId",
                table: "CampgroundImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CampgroundReviews_Campgrounds_CampgroundId",
                table: "CampgroundReviews");

            migrationBuilder.AlterColumn<int>(
                name: "CampgroundId",
                table: "CampgroundReviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CampgroundId",
                table: "CampgroundImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CampgroundImages_Campgrounds_CampgroundId",
                table: "CampgroundImages",
                column: "CampgroundId",
                principalTable: "Campgrounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampgroundReviews_Campgrounds_CampgroundId",
                table: "CampgroundReviews",
                column: "CampgroundId",
                principalTable: "Campgrounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampgroundImages_Campgrounds_CampgroundId",
                table: "CampgroundImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CampgroundReviews_Campgrounds_CampgroundId",
                table: "CampgroundReviews");

            migrationBuilder.AlterColumn<int>(
                name: "CampgroundId",
                table: "CampgroundReviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CampgroundId",
                table: "CampgroundImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CampgroundImages_Campgrounds_CampgroundId",
                table: "CampgroundImages",
                column: "CampgroundId",
                principalTable: "Campgrounds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CampgroundReviews_Campgrounds_CampgroundId",
                table: "CampgroundReviews",
                column: "CampgroundId",
                principalTable: "Campgrounds",
                principalColumn: "Id");
        }
    }
}
