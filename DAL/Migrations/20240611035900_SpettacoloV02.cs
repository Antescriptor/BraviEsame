using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SpettacoloV02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prenotazioni_spettacolo_fk_tavolo",
                table: "prenotazioni");

            migrationBuilder.RenameTable(
                name: "spettacolo",
                newName: "spettacoli");

            migrationBuilder.RenameColumn(
                name: "fk_tavolo",
                table: "prenotazioni",
                newName: "fk_spettacolo");

            migrationBuilder.RenameIndex(
                name: "IX_prenotazioni_fk_tavolo",
                table: "prenotazioni",
                newName: "IX_prenotazioni_fk_spettacolo");

            migrationBuilder.AddForeignKey(
                name: "FK_prenotazioni_spettacoli_fk_spettacolo",
                table: "prenotazioni",
                column: "fk_spettacolo",
                principalTable: "spettacoli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prenotazioni_spettacoli_fk_spettacolo",
                table: "prenotazioni");

            migrationBuilder.RenameTable(
                name: "spettacoli",
                newName: "spettacolo");

            migrationBuilder.RenameColumn(
                name: "fk_spettacolo",
                table: "prenotazioni",
                newName: "fk_tavolo");

            migrationBuilder.RenameIndex(
                name: "IX_prenotazioni_fk_spettacolo",
                table: "prenotazioni",
                newName: "IX_prenotazioni_fk_tavolo");

            migrationBuilder.AddForeignKey(
                name: "FK_prenotazioni_spettacolo_fk_tavolo",
                table: "prenotazioni",
                column: "fk_tavolo",
                principalTable: "spettacolo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
