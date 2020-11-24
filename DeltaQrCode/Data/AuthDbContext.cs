using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DeltaQrCode
{
    public partial class AuthDbContext : DbContext
    {
        public AuthDbContext()
        {
        }

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CaUsers> CaUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CaUsers>(entity =>
            {
                entity.ToTable("ca_users");

                entity.HasIndex(e => e.Id)
                    .HasName("id")
                    .IsUnique();

                entity.HasIndex(e => e.UserAccount)
                    .HasName("user_account")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

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
                    .HasColumnType("int(11)")
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
        }
    }
}
