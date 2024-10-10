using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Watchlist.Migrations
{
    public partial class currentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUser_AspNetUsers_UserId",
                table: "MoviesUser");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUser_Movies_MovieId",
                table: "MoviesUser");

            migrationBuilder.DropIndex(
                name: "IX_MoviesUser_MovieId",
                table: "MoviesUser");

            migrationBuilder.DropIndex(
                name: "IX_MoviesUser_UserId",
                table: "MoviesUser");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "MoviesUser");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MoviesUser");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesUser_IdMovie",
                table: "MoviesUser",
                column: "IdMovie");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesUser_AspNetUsers_IdUser",
                table: "MoviesUser",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesUser_Movies_IdMovie",
                table: "MoviesUser",
                column: "IdMovie",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUser_AspNetUsers_IdUser",
                table: "MoviesUser");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUser_Movies_IdMovie",
                table: "MoviesUser");

            migrationBuilder.DropIndex(
                name: "IX_MoviesUser_IdMovie",
                table: "MoviesUser");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "MoviesUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MoviesUser",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesUser_MovieId",
                table: "MoviesUser",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesUser_UserId",
                table: "MoviesUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesUser_AspNetUsers_UserId",
                table: "MoviesUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesUser_Movies_MovieId",
                table: "MoviesUser",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
