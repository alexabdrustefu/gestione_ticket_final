﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using gestione_ticket.Data;

#nullable disable

namespace gestione_ticket_final.Migrations
{
    [DbContext(typeof(gestione_ticket_finalContext))]
    [Migration("20240503141026_migrazione3")]
    partial class migrazione3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("gestione_ticket_final.Models.LavorazioneTicket", b =>
                {
                    b.Property<int?>("LavorazioneTicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_ticket_lavorazione");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("LavorazioneTicketId"));

                    b.Property<DateTime>("Data_presa_incarico")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_presa_incarico");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Ora_presa_incarico")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ora_presa_incarico");

                    b.Property<int?>("TicketId")
                        .HasColumnType("int")
                        .HasColumnName("id_ticket");

                    b.Property<int?>("UserId_utente")
                        .HasColumnType("int");

                    b.Property<int?>("UtenteId")
                        .HasColumnType("int")
                        .HasColumnName("id_utente");

                    b.Property<string>("motivazione")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("motivazione");

                    b.HasKey("LavorazioneTicketId");

                    b.HasIndex("TicketId");

                    b.HasIndex("UserId_utente");

                    b.ToTable("LavorazioneTicket");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.Prodotto", b =>
                {
                    b.Property<int>("ProdottoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdottoId"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TipologiaProdottoId")
                        .HasColumnType("int")
                        .HasColumnName("TipoProdottoId");

                    b.HasKey("ProdottoId");

                    b.HasIndex("TipologiaProdottoId");

                    b.ToTable("Prodotto");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.Ticket", b =>
                {
                    b.Property<int?>("Id_ticket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id_ticket"));

                    b.Property<bool>("AssegnaAllUtenteLoggato")
                        .HasColumnType("bit")
                        .HasColumnName("assegna_utente_loggato");

                    b.Property<DateTime?>("Data_apertura")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_apertura");

                    b.Property<DateTime?>("Data_chiusura")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_chiusura");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Descrizione")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("descrizione");

                    b.Property<string>("Ora_apertura")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ora_apertura");

                    b.Property<string>("Ora_chiusura")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ora_chiusura");

                    b.Property<int?>("ProdottoId")
                        .HasColumnType("int")
                        .HasColumnName("prodottoId");

                    b.Property<string>("Soluzione")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("soluzione");

                    b.Property<int?>("Stato")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UsereId");

                    b.HasKey("Id_ticket");

                    b.HasIndex("ProdottoId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UsereId] IS NOT NULL");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.TipologiaProdotto", b =>
                {
                    b.Property<int>("Id_tipologia_prodotto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_tipologia_prodotto"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_tipologia_prodotto");

                    b.ToTable("TipologiaProdotto");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.User", b =>
                {
                    b.Property<int>("Id_utente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_utente"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Cognome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLoggedIn")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordBase64")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("Ruolo")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_utente");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.LavorazioneTicket", b =>
                {
                    b.HasOne("gestione_ticket_final.Models.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId");

                    b.HasOne("gestione_ticket_final.Models.User", "User")
                        .WithMany("Lavorazioni_ticket")
                        .HasForeignKey("UserId_utente");

                    b.Navigation("Ticket");

                    b.Navigation("User");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.Prodotto", b =>
                {
                    b.HasOne("gestione_ticket_final.Models.TipologiaProdotto", "TipologiaProdotto")
                        .WithMany("Prodotti")
                        .HasForeignKey("TipologiaProdottoId");

                    b.Navigation("TipologiaProdotto");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.Ticket", b =>
                {
                    b.HasOne("gestione_ticket_final.Models.Prodotto", "Prodotto")
                        .WithMany("Tickets")
                        .HasForeignKey("ProdottoId");

                    b.HasOne("gestione_ticket_final.Models.User", "User")
                        .WithOne("Tickets")
                        .HasForeignKey("gestione_ticket_final.Models.Ticket", "UserId");

                    b.Navigation("Prodotto");

                    b.Navigation("User");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.Prodotto", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.TipologiaProdotto", b =>
                {
                    b.Navigation("Prodotti");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.User", b =>
                {
                    b.Navigation("Lavorazioni_ticket");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
