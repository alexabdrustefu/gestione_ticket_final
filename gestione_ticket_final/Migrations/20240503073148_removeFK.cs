using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestione_ticket_final.Migrations
{
    /// <inheritdoc />
    public partial class removeFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ticket_UsereId",
                table: "Ticket");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UsereId",
                table: "Ticket",
                column: "UsereId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ticket_UsereId",
                table: "Ticket");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UsereId",
                table: "Ticket",
                column: "UsereId",
                unique: true,
                filter: "[UsereId] IS NOT NULL");
        }
    }
}
