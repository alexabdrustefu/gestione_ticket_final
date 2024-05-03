using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestione_ticket_final.Migrations
{
    /// <inheritdoc />
    public partial class addDelted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodotto_TipologiaProdotto_TipoProdottoId",
                table: "Prodotto");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "TipologiaProdotto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Ticket",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "TipoProdottoId",
                table: "Prodotto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Prodotto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "LavorazioneTicket",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotto_TipologiaProdotto_TipoProdottoId",
                table: "Prodotto",
                column: "TipoProdottoId",
                principalTable: "TipologiaProdotto",
                principalColumn: "Id_tipologia_prodotto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodotto_TipologiaProdotto_TipoProdottoId",
                table: "Prodotto");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "TipologiaProdotto");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Prodotto");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "LavorazioneTicket");

            migrationBuilder.AlterColumn<int>(
                name: "TipoProdottoId",
                table: "Prodotto",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotto_TipologiaProdotto_TipoProdottoId",
                table: "Prodotto",
                column: "TipoProdottoId",
                principalTable: "TipologiaProdotto",
                principalColumn: "Id_tipologia_prodotto",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
