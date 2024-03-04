using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMovies.Migrations
{
    /// <inheritdoc />
    public partial class AddedIMDbString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IMDbLink",
                table: "TvShows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IMDbLink",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMDbLink",
                table: "TvShows");

            migrationBuilder.DropColumn(
                name: "IMDbLink",
                table: "Movies");
        }
    }
}
