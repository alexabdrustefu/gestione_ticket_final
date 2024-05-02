﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using gestione_ticket.Data;

#nullable disable

namespace gestione_ticket_final.Migrations
{
    [DbContext(typeof(gestione_ticket_finalContext))]
    partial class gestione_ticket_finalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("gestione_ticket_final.Models.LavorazioneTicket", b =>
                {
                    b.Property<int>("LavorazioneTicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_ticket_lavorazione");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LavorazioneTicketId"));

                    b.Property<DateTime>("Data_presa_incarico")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_presa_incarico");

                    b.Property<string>("Ora_presa_incarico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ora_presa_incarico");

                    b.Property<int>("TicketId")
                        .HasColumnType("int")
                        .HasColumnName("id_ticket");

                    b.Property<int>("UtenteId")
                        .HasColumnType("int")
                        .HasColumnName("id_utente");

                    b.Property<string>("motivazione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("motivazione");

                    b.HasKey("LavorazioneTicketId");

                    b.ToTable("LavorazioneTicket");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.LoginModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordBase64")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LoginModel");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.Prodotto", b =>
                {
                    b.Property<int>("ProdottoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdottoId"));

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoProdottoId")
                        .HasColumnType("int")
                        .HasColumnName("tipoProdottoId");

                    b.HasKey("ProdottoId");

                    b.ToTable("Prodotto");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.Ticket", b =>
                {
                    b.Property<int?>("Id_ticket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id_ticket"));

                    b.Property<DateTime?>("Data_apertura")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_apertura");

                    b.Property<DateTime?>("Data_chiusura")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_chiusura");

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

                    b.Property<int?>("UtenteId")
                        .HasColumnType("int")
                        .HasColumnName("utente_id");

                    b.HasKey("Id_ticket");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("gestione_ticket_final.Models.TipologiaProdotto", b =>
                {
                    b.Property<int>("Id_tipologia_prodotto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_tipologia_prodotto"));

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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordBase64")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Ruolo")
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
#pragma warning restore 612, 618
        }
    }
}
