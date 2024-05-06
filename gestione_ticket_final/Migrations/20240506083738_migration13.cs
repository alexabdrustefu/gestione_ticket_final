using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestione_ticket_final.Migrations
{
    /// <inheritdoc />
    public partial class migration13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipologiaProdotto",
                columns: table => new
                {
                    Id_tipologia_prodotto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipologiaProdotto", x => x.Id_tipologia_prodotto);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ruolo = table.Column<int>(type: "int", nullable: true),
                    IsLoggedIn = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    HasChangedPassword = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Prodotto",
                columns: table => new
                {
                    ProdottoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoProdottoId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodotto", x => x.ProdottoId);
                    table.ForeignKey(
                        name: "FK_Prodotto_TipologiaProdotto_TipoProdottoId",
                        column: x => x.TipoProdottoId,
                        principalTable: "TipologiaProdotto",
                        principalColumn: "Id_tipologia_prodotto");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id_ticket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data_apertura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ora_apertura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data_chiusura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ora_chiusura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    prodottoId = table.Column<int>(type: "int", nullable: true),
                    soluzione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    assegna_utente_loggato = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id_ticket);
                    table.ForeignKey(
                        name: "FK_Ticket_Prodotto_prodottoId",
                        column: x => x.prodottoId,
                        principalTable: "Prodotto",
                        principalColumn: "ProdottoId");
                    table.ForeignKey(
                        name: "FK_Ticket_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "LavorazioneTicket",
                columns: table => new
                {
                    id_ticket_lavorazione = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_utente = table.Column<int>(type: "int", nullable: true),
                    id_ticket = table.Column<int>(type: "int", nullable: true),
                    data_presa_incarico = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ora_presa_incarico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivazione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LavorazioneTicket", x => x.id_ticket_lavorazione);
                    table.ForeignKey(
                        name: "FK_LavorazioneTicket_Ticket_id_ticket",
                        column: x => x.id_ticket,
                        principalTable: "Ticket",
                        principalColumn: "Id_ticket");
                    table.ForeignKey(
                        name: "FK_LavorazioneTicket_Users_id_utente",
                        column: x => x.id_utente,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LavorazioneTicket_id_ticket",
                table: "LavorazioneTicket",
                column: "id_ticket");

            migrationBuilder.CreateIndex(
                name: "IX_LavorazioneTicket_id_utente",
                table: "LavorazioneTicket",
                column: "id_utente");

            migrationBuilder.CreateIndex(
                name: "IX_Prodotto_TipoProdottoId",
                table: "Prodotto",
                column: "TipoProdottoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_prodottoId",
                table: "Ticket",
                column: "prodottoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LavorazioneTicket");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Prodotto");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TipologiaProdotto");
        }
    }
}
