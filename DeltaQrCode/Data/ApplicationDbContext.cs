using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DeltaQrCode.Models;

namespace DeltaQrCode.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<CaClient> CaClient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CaClient>(entity =>
            {
                entity.ToTable("ca_client");

                entity.HasIndex(e => e.Id)
                    .HasName("id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnFabricatie).HasColumnName("an_fabricatie");

                entity.Property(e => e.CostAbonament)
                    .HasColumnName("cost_abonament")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Custodie).HasColumnName("custodie");

                entity.Property(e => e.DataExpirareAbonament)
                    .HasColumnName("data_expirare_abonament")
                    .HasColumnType("date");

                entity.Property(e => e.DataFacturare)
                    .HasColumnName("data_facturare")
                    .HasColumnType("date");

                entity.Property(e => e.DataInitiala)
                    .HasColumnName("data_initiala")
                    .HasColumnType("date");

                entity.Property(e => e.DataInsert)
                    .HasColumnName("data_insert")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataInstalari)
                    .HasColumnName("data_instalari")
                    .HasColumnType("datetime");

                entity.Property(e => e.FirmaPrestatoare).HasColumnName("firma_prestatoare");

                entity.Property(e => e.IdManopera)
                    .IsRequired()
                    .HasColumnName("id_manopera")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Instalator)
                    .IsRequired()
                    .HasColumnName("instalator")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.KmBord).HasColumnName("km_bord");

                entity.Property(e => e.KmEfectuati)
                    .HasColumnName("km_efectuati")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LocatieMontaj)
                    .IsRequired()
                    .HasColumnName("locatie_montaj")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.MarcaMasina)
                    .IsRequired()
                    .HasColumnName("marca_masina")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Nefacturat).HasColumnName("nefacturat");

                entity.Property(e => e.NoteInstalare)
                    .IsRequired()
                    .HasColumnName("note_instalare")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NrBucati)
                    .IsRequired()
                    .HasColumnName("nr_bucati")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.NrFactura)
                    .IsRequired()
                    .HasColumnName("nr_factura")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.NrFacturaAbonament)
                    .IsRequired()
                    .HasColumnName("nr_factura_abonament")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NrFisa)
                    .IsRequired()
                    .HasColumnName("nr_fisa")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.NrMasina)
                    .IsRequired()
                    .HasColumnName("nr_masina")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.NrTelefon)
                    .IsRequired()
                    .HasColumnName("nr_telefon")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NumeClient)
                    .IsRequired()
                    .HasColumnName("nume_client")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.PerioadaContractuala)
                    .HasColumnName("perioada_contractuala")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ReprezentantClient)
                    .IsRequired()
                    .HasColumnName("reprezentant_client")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ReprezentantClientMail)
                    .IsRequired()
                    .HasColumnName("reprezentant_client_mail")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ReprezentantClientTelefon)
                    .IsRequired()
                    .HasColumnName("reprezentant_client_telefon")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.SeriaSim)
                    .IsRequired()
                    .HasColumnName("seria_sim")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.SerieGps)
                    .IsRequired()
                    .HasColumnName("serie_gps")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.SerieSasiu)
                    .IsRequired()
                    .HasColumnName("serie_sasiu")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Stare)
                    .IsRequired()
                    .HasColumnName("stare")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.TipAparat)
                    .IsRequired()
                    .HasColumnName("tip_aparat")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TipAuto)
                    .IsRequired()
                    .HasColumnName("tip_auto")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.TipFactura)
                    .IsRequired()
                    .HasColumnName("tip_factura")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.TipServiciu)
                    .IsRequired()
                    .HasColumnName("tip_serviciu")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.TipVanzare)
                    .IsRequired()
                    .HasColumnName("tip_vanzare")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.UserAccount)
                    .IsRequired()
                    .HasColumnName("user_account")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Vin)
                    .IsRequired()
                    .HasColumnName("vin")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.ZileExpirareAbonament).HasColumnName("zile_expirare_abonament");
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
