using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestione_ticket_final.Migrations
{
    /// <inheritdoc />
    public partial class migrazione2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "assegna_utente_loggato",
                table: "Ticket",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assegna_utente_loggato",
                table: "Ticket");
        }
    }
}
