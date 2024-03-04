using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMovies.Migrations
{
    /// <inheritdoc />
    public partial class AddedBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgBanner",
                table: "TvShows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgBanner",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgBanner",
                table: "TvShows");

            migrationBuilder.DropColumn(
                name: "ImgBanner",
                table: "Movies");
        }
    }
}
