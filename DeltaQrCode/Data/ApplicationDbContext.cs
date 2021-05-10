using DeltaQrCode.Models;
using Microsoft.EntityFrameworkCore;

namespace DeltaQrCode.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CaAppointments> CaAppointments { get; set; }
        public virtual DbSet<CaClient> CaClient { get; set; }
        public virtual DbSet<CaLogOperatiune> CaLogOperatiune { get; set; }
        public virtual DbSet<CaMarca> CaMarca { get; set; }
        public virtual DbSet<CaFlota> CaFlota { get; set; }
        public virtual DbSet<CaOperatiuneSchimbAnvelope> CaOperatiuneSchimbAnvelope { get; set; }
        public virtual DbSet<CaServicetypes> CaServicetypes { get; set; }
        public virtual DbSet<CaAnvelopa> CaAnvelopa { get; set; }
        public virtual DbSet<CaHotelPositions> CaHotelPositions { get; set; }
        public virtual DbSet<CaUsers> CaUsers { get; set; }
        public virtual DbSet<HistoryAnvelope> HistoryAnvelope { get; set; }
        public virtual DbSet<PasOperatiune> PasOperatiune { get; set; }

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

            modelBuilder.Entity<CaAppointments>(entity =>
                    {
                        entity.ToTable("ca_appointments");

                        entity.HasIndex(e => e.Id)
                            .HasName("id_UNIQUE")
                            .IsUnique();

                        entity.Property(e => e.Id).HasColumnName("id");

                        entity.Property(e => e.Confirmed).HasColumnType("bit(1)");

                        entity.Property(e => e.ConfirmedCode)
                            .IsRequired()
                            .HasColumnType("varchar(100)");

                        entity.Property(e => e.ConfirmedDate).HasColumnType("datetime");

                        entity.Property(e => e.DataAppointment).HasColumnType("datetime");

                        entity.Property(e => e.DataIntroducere).HasColumnType("datetime");

                        entity.Property(e => e.Deleted).HasColumnType("bit(1)");

                        entity.Property(e => e.EmailClient).HasColumnType("varchar(100)");

                        entity.Property(e => e.LastModified).HasColumnType("datetime");

                        entity.Property(e => e.RampId)
                            .IsRequired()
                            .HasColumnType("int");

                        entity.Property(e => e.DurataInMinute)
                            .IsRequired()
                            .HasColumnType("int");

                        entity.Property(e => e.ServiciuId)
                            .IsRequired()
                            .HasColumnType("int");

                        entity.Property(e => e.NumarInmatriculare)
                            .IsRequired()
                            .HasColumnType("varchar(45)");

                        entity.Property(e => e.NumarTelefon)
                            .IsRequired()
                            .HasColumnType("varchar(45)");

                        entity.Property(e => e.NumeClient)
                            .IsRequired()
                            .HasColumnType("varchar(45)");

                        entity.Property(e => e.Observatii)
                            .HasColumnType("varchar(256)");

                        entity.Property(e => e.OraInceput).HasColumnType("time");
                        entity.Property(e => e.ChangedByClient).HasColumnType("bit(1)");

                    });

            modelBuilder.Entity<CaLogOperatiune>(entity =>
            {
                entity.ToTable("ca_log_operatiune");

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.AjunsLaTimp).HasColumnType("bit(1)");
            });

            modelBuilder.Entity<CaMarca>(entity =>
            {
                entity.ToTable("ca_marca");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<CaFlota>(entity =>
            {
                entity.ToTable("ca_flota");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<CaOperatiuneSchimbAnvelope>(entity =>
            {
                entity.ToTable("ca_operatiune_schimb_anvelope");

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.DataSchimb).HasColumnType("datetime");

                entity.Property(e => e.NumarInmatriculare)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Observatii).HasColumnType("varchar(256)");

                entity.Property(e => e.OperatiuneFinalizata).HasColumnType("bit(1)");

                entity.Property(e => e.OraInceput).HasColumnType("time");

                entity.Property(e => e.OraSfarsit).HasColumnType("time");

                entity.Property(e => e.PersoanaContact)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<CaServicetypes>(entity =>
            {
                entity.ToTable("ca_servicetypes");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasColumnName("label")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<CaAnvelopa>(entity =>
            {
                entity.ToTable("ca_anvelopa");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataUltimaModificare).HasColumnType("datetime");

                entity.Property(e => e.Deleted).HasColumnType("bit(1)");

                entity.Property(e => e.Dimensiuni)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NumarInmatriculare)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.NumarTelefon)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.NumeClient)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Observatii).HasColumnType("varchar(100)");

                entity.Property(e => e.PozitiePeMasina).HasColumnType("varchar(45)");

                entity.Property(e => e.SerieSasiu).HasColumnType("varchar(45)");

                entity.Property(e => e.StatusCurent)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.TipSezon)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Uzura)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<CaUsers>(entity =>
            {
                entity.ToTable("ca_users");

                entity.HasIndex(e => e.Id)
                    .HasName("id")
                    .IsUnique();

                entity.HasIndex(e => e.UserAccount)
                    .HasName("user_account")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UserAccount)
                    .IsRequired()
                    .HasColumnName("user_account")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.UserCompany)
                    .IsRequired()
                    .HasColumnName("user_company")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.UserEmailAddress)
                    .IsRequired()
                    .HasColumnName("user_email_address")
                    .HasColumnType("varchar(60)");

                entity.Property(e => e.UserFirstName)
                    .IsRequired()
                    .HasColumnName("user_first_name")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.UserLastName)
                    .IsRequired()
                    .HasColumnName("user_last_name")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.UserLock)
                    .HasColumnName("user_lock")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UserMobile)
                    .IsRequired()
                    .HasColumnName("user_mobile")
                    .HasColumnType("varchar(12)");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("user_password")
                    .HasColumnType("varchar(120)");

                entity.Property(e => e.UserPhone)
                    .HasColumnName("user_phone")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.UserRights)
                    .IsRequired()
                    .HasColumnName("user_rights")
                    .HasColumnType("text");

                entity.Property(e => e.UserRightsAdmin)
                    .IsRequired()
                    .HasColumnName("user_rights_admin")
                    .HasColumnType("varchar(4)")
                    .HasDefaultValueSql("'0000'");

                entity.Property(e => e.UserRightsBycompany)
                    .IsRequired()
                    .HasColumnName("user_rights_bycompany")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<HistoryAnvelope>(entity =>
            {
                entity.ToTable("history_anvelope");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Changes)
                    .IsRequired()
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.DataModificare).HasColumnType("datetime");
            });

            modelBuilder.Entity<CaHotelPositions>(entity =>
            {
                entity.ToTable("ca_hotel_positions");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Interval)
                    .IsRequired()
                    .HasColumnName("interval")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Locuriocupate).HasColumnName("locuriocupate");

                entity.Property(e => e.Ocupat)
                    .HasColumnName("ocupat")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Pozitie)
                    .IsRequired()
                    .HasColumnName("pozitie")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Rand)
                    .IsRequired()
                    .HasColumnName("rand")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<PasOperatiune>(entity =>
            {
                entity.ToTable("pas_operatiune");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.InsertedDate)
                    .HasColumnName("insertedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.OperatiuneId)
                    .HasColumnName("operatiuneId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Pas)
                    .HasColumnName("pas")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SavedData)
                    .IsRequired()
                    .HasColumnName("savedData")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<CaSetAnvelope>(entity =>
            {
                entity.ToTable("ca_set_anvelope");

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.NumeSet)
                    .IsRequired()
                    .HasColumnName("Nume_Set")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<IstoricStatusAnvelopa>(entity =>
            {
                entity.ToTable("istoric_status_anvelopa");

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.NewStatus)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.OldStatus)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<CaMasina>(entity =>
            {
                entity.ToTable("ca_masina");

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.NumarInmatriculare).HasColumnType("varchar(45)");

                entity.Property(e => e.SerieSasiu).HasColumnType("varchar(45)");

                entity.Property(e => e.TipVehicul)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<CaClientHotel>(entity =>
            {
                entity.ToTable("ca_client_hotel");

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.NumarTelefon)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.NumeClient)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Sofer).HasColumnType("varchar(45)");
            });
        }
    }
}
