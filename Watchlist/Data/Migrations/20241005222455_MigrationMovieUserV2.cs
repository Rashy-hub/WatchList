using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Watchlist.Data.Migrations
{
    public partial class MigrationMovieUserV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUser_Movies_FilmId",
                table: "MoviesUser");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "MoviesUser",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "FilmId",
                table: "MoviesUser",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviesUser_FilmId",
                table: "MoviesUser",
                newName: "IX_MoviesUser_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesUser_Movies_MovieId",
                table: "MoviesUser",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUser_Movies_MovieId",
                table: "MoviesUser");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "MoviesUser",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "MoviesUser",
                newName: "FilmId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviesUser_MovieId",
                table: "MoviesUser",
                newName: "IX_MoviesUser_FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesUser_Movies_FilmId",
                table: "MoviesUser",
                column: "FilmId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
