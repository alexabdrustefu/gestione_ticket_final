using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestione_ticket_final.Migrations
{
    /// <inheritdoc />
    public partial class migrazione8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Users_UsereId",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "UsereId",
                table: "Ticket",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_UsereId",
                table: "Ticket",
                newName: "IX_Ticket_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Users_UserId",
                table: "Ticket",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id_utente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Users_UserId",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Ticket",
                newName: "UsereId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                newName: "IX_Ticket_UsereId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Users_UsereId",
                table: "Ticket",
                column: "UsereId",
                principalTable: "Users",
                principalColumn: "Id_utente");
        }
    }
}
