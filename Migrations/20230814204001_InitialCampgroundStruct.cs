using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yelpcamp.Migrations
{
    public partial class InitialCampgroundStruct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campgrounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Geometry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campgrounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campgrounds_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CampgroundImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampgroundsId = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampgroundImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampgroundImages_Campgrounds_CampgroundsId",
                        column: x => x.CampgroundsId,
                        principalTable: "Campgrounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampgroundReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampgroundsId = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AuthorUserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampgroundReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampgroundReviews_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CampgroundReviews_Campgrounds_CampgroundsId",
                        column: x => x.CampgroundsId,
                        principalTable: "Campgrounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampgroundImages_CampgroundsId",
                table: "CampgroundImages",
                column: "CampgroundsId");

            migrationBuilder.CreateIndex(
                name: "IX_CampgroundReviews_AuthorId",
                table: "CampgroundReviews",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CampgroundReviews_CampgroundsId",
                table: "CampgroundReviews",
                column: "CampgroundsId");

            migrationBuilder.CreateIndex(
                name: "IX_Campgrounds_AuthorId",
                table: "Campgrounds",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampgroundImages");

            migrationBuilder.DropTable(
                name: "CampgroundReviews");

            migrationBuilder.DropTable(
                name: "Campgrounds");
        }
    }
}
