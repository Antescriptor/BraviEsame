﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(GestionaleDbContext))]
    [Migration("20240611035938_PrenotazioneV02")]
    partial class PrenotazioneV02
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.Cliente", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("cognome");

                    b.Property<string>("Email")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("nome");

                    b.Property<string>("Telefono")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("telefono");

                    b.HasKey("Id")
                        .HasName("pk_clienti");

                    b.ToTable("clienti", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Prenotazione", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<DateTime>("DataEOraPrenotazione")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("data_e_ora_prenotazione");

                    b.Property<uint>("IdCliente")
                        .HasColumnType("int unsigned")
                        .HasColumnName("fk_cliente");

                    b.Property<uint>("IdSpettacolo")
                        .HasColumnType("int unsigned")
                        .HasColumnName("fk_spettacolo");

                    b.Property<string>("Posto")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("posto");

                    b.Property<decimal>("Prezzo")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("prezzo");

                    b.HasKey("Id")
                        .HasName("pk_prenotazioni");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdSpettacolo");

                    b.ToTable("prenotazioni", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Spettacolo", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<uint>("Id"));

                    b.Property<DateTime>("DataEOra")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("data_e_ora");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("descrizione");

                    b.Property<uint>("Durata")
                        .HasColumnType("int unsigned")
                        .HasColumnName("durata");

                    b.Property<decimal>("PrezzoBase")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("prezzo_base");

                    b.Property<string>("Titolo")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("titolo");

                    b.HasKey("Id")
                        .HasName("pk_spettacoli");

                    b.ToTable("spettacoli", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Prenotazione", b =>
                {
                    b.HasOne("DAL.Models.Cliente", "Cliente")
                        .WithMany("Prenotazioni")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DAL.Models.Spettacolo", "Spettacolo")
                        .WithMany("Prenotazioni")
                        .HasForeignKey("IdSpettacolo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Spettacolo");
                });

            modelBuilder.Entity("DAL.Models.Cliente", b =>
                {
                    b.Navigation("Prenotazioni");
                });

            modelBuilder.Entity("DAL.Models.Spettacolo", b =>
                {
                    b.Navigation("Prenotazioni");
                });
#pragma warning restore 612, 618
        }
    }
}
