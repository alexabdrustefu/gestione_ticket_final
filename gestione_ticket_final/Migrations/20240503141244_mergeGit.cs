using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestione_ticket_final.Migrations
{
    /// <inheritdoc />
    public partial class mergeGit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LavorazioneTicket_Ticket_id_ticket",
                table: "LavorazioneTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_LavorazioneTicket_Users_id_utente",
                table: "LavorazioneTicket");

            migrationBuilder.AlterColumn<string>(
                name: "ora_presa_incarico",
                table: "LavorazioneTicket",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "motivazione",
                table: "LavorazioneTicket",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "id_utente",
                table: "LavorazioneTicket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "id_ticket",
                table: "LavorazioneTicket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LavorazioneTicket_Ticket_id_ticket",
                table: "LavorazioneTicket",
                column: "id_ticket",
                principalTable: "Ticket",
                principalColumn: "Id_ticket");

            migrationBuilder.AddForeignKey(
                name: "FK_LavorazioneTicket_Users_id_utente",
                table: "LavorazioneTicket",
                column: "id_utente",
                principalTable: "Users",
                principalColumn: "Id_utente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LavorazioneTicket_Ticket_id_ticket",
                table: "LavorazioneTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_LavorazioneTicket_Users_id_utente",
                table: "LavorazioneTicket");

            migrationBuilder.AlterColumn<string>(
                name: "ora_presa_incarico",
                table: "LavorazioneTicket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "motivazione",
                table: "LavorazioneTicket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_utente",
                table: "LavorazioneTicket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_ticket",
                table: "LavorazioneTicket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LavorazioneTicket_Ticket_id_ticket",
                table: "LavorazioneTicket",
                column: "id_ticket",
                principalTable: "Ticket",
                principalColumn: "Id_ticket",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LavorazioneTicket_Users_id_utente",
                table: "LavorazioneTicket",
                column: "id_utente",
                principalTable: "Users",
                principalColumn: "Id_utente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
