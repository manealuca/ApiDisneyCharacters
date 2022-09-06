using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace disneyapi.Migrations
{
    public partial class ModificandoDelete3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEntityMovieEntity_Characters_MoviesMovieId",
                table: "CharacterEntityMovieEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEntityMovieEntity_Movies_MoviesMovieId",
                table: "CharacterEntityMovieEntity");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEntityMovieEntity_Characters_MoviesMovieId",
                table: "CharacterEntityMovieEntity",
                column: "MoviesMovieId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEntityMovieEntity_Movies_MoviesMovieId",
                table: "CharacterEntityMovieEntity",
                column: "MoviesMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEntityMovieEntity_Characters_MoviesMovieId",
                table: "CharacterEntityMovieEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEntityMovieEntity_Movies_MoviesMovieId",
                table: "CharacterEntityMovieEntity");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEntityMovieEntity_Characters_MoviesMovieId",
                table: "CharacterEntityMovieEntity",
                column: "MoviesMovieId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEntityMovieEntity_Movies_MoviesMovieId",
                table: "CharacterEntityMovieEntity",
                column: "MoviesMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
