using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yelpcamp.Migrations
{
    public partial class ReengineerUserStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampgroundImages_Campgrounds_CampgroundsId",
                table: "CampgroundImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CampgroundReviews_AspNetUsers_AuthorId",
                table: "CampgroundReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_CampgroundReviews_Campgrounds_CampgroundsId",
                table: "CampgroundReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Campgrounds_AspNetUsers_AuthorId",
                table: "Campgrounds");

            migrationBuilder.DropIndex(
                name: "IX_Campgrounds_AuthorId",
                table: "Campgrounds");

            migrationBuilder.DropIndex(
                name: "IX_CampgroundReviews_AuthorId",
                table: "CampgroundReviews");

            migrationBuilder.DropIndex(
                name: "IX_CampgroundReviews_CampgroundsId",
                table: "CampgroundReviews");

            migrationBuilder.DropIndex(
                name: "IX_CampgroundImages_CampgroundsId",
                table: "CampgroundImages");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "CampgroundReviews");

            migrationBuilder.AddColumn<string>(
                name: "AuthorUserId",
                table: "Campgrounds",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorUserName",
                table: "Campgrounds",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorUserId",
                table: "CampgroundReviews",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CampgroundId",
                table: "CampgroundReviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CampgroundId",
                table: "CampgroundImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CampgroundReviews_CampgroundId",
                table: "CampgroundReviews",
                column: "CampgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_CampgroundImages_CampgroundId",
                table: "CampgroundImages",
                column: "CampgroundId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampgroundImages_Campgrounds_CampgroundId",
                table: "CampgroundImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CampgroundReviews_Campgrounds_CampgroundId",
                table: "CampgroundReviews");

            migrationBuilder.DropIndex(
                name: "IX_CampgroundReviews_CampgroundId",
                table: "CampgroundReviews");

            migrationBuilder.DropIndex(
                name: "IX_CampgroundImages_CampgroundId",
                table: "CampgroundImages");

            migrationBuilder.DropColumn(
                name: "AuthorUserId",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "AuthorUserName",
                table: "Campgrounds");

            migrationBuilder.DropColumn(
                name: "AuthorUserId",
                table: "CampgroundReviews");

            migrationBuilder.DropColumn(
                name: "CampgroundId",
                table: "CampgroundReviews");

            migrationBuilder.DropColumn(
                name: "CampgroundId",
                table: "CampgroundImages");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Campgrounds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "CampgroundReviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campgrounds_AuthorId",
                table: "Campgrounds",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CampgroundReviews_AuthorId",
                table: "CampgroundReviews",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CampgroundReviews_CampgroundsId",
                table: "CampgroundReviews",
                column: "CampgroundsId");

            migrationBuilder.CreateIndex(
                name: "IX_CampgroundImages_CampgroundsId",
                table: "CampgroundImages",
                column: "CampgroundsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CampgroundImages_Campgrounds_CampgroundsId",
                table: "CampgroundImages",
                column: "CampgroundsId",
                principalTable: "Campgrounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampgroundReviews_AspNetUsers_AuthorId",
                table: "CampgroundReviews",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CampgroundReviews_Campgrounds_CampgroundsId",
                table: "CampgroundReviews",
                column: "CampgroundsId",
                principalTable: "Campgrounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campgrounds_AspNetUsers_AuthorId",
                table: "Campgrounds",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
