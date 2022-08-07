using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Petify.Data.DBModels
{
    public partial class PetifyLiveContext : DbContext
    {
        public PetifyLiveContext()
        {
        }

        public PetifyLiveContext(DbContextOptions<PetifyLiveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Allergy> Allergies { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Billing> Billings { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<FeedBack> FeedBacks { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<Grooming> Groomings { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Pet> Pets { get; set; } = null!;
        public virtual DbSet<PetBreed> PetBreeds { get; set; } = null!;
        public virtual DbSet<PetType> PetTypes { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<UserImage> UserImages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-U1HET32\\SQLEXPRESS;Database=PetifyLive;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Allergy>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AllergyName).HasMaxLength(100);
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserId).ValueGeneratedOnAdd();

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Billing>(entity =>
            {
                entity.ToTable("Billing");

                entity.HasIndex(e => e.PriceUnit, "IX_Billing_PetId");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.PriceUnit).HasColumnType("money");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasIndex(e => e.CheckOutDate, "IX_PetsAllergy_AllergiesId");

                entity.HasIndex(e => e.CheckInDate, "IX_PetsAllergy_PetId");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CheckInDate).HasColumnType("datetime");

                entity.Property(e => e.CheckOutDate).HasColumnType("datetime");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Billing)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.BillingId)
                    .HasConstraintName("FK_Bookings_Billing");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_Bookings_Services");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Bookings_AspNetUsers");
            });

            modelBuilder.Entity<FeedBack>(entity =>
            {
                entity.ToTable("FeedBack");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.Message).HasMaxLength(150);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FeedBacks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_FeedBack_AspNetUsers");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.Property(e => e.GenderType).HasMaxLength(20);
            });

            modelBuilder.Entity<Grooming>(entity =>
            {
                entity.ToTable("Grooming");

                entity.HasIndex(e => e.UserId, "IX_PetServices_PetId");

                entity.HasIndex(e => e.BillingId, "IX_PetServices_ServicesId");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.HasOne(d => d.Billing)
                    .WithMany(p => p.Groomings)
                    .HasForeignKey(d => d.BillingId)
                    .HasConstraintName("FK_Grooming_Billing");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Groomings)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_Grooming_Services");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Groomings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Grooming_AspNetUsers");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasIndex(e => e.BillingId, "IX_Payments_BillingId");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.TotalAmount).HasColumnType("money");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Billing)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BillingId)
                    .HasConstraintName("FK_Payments_Billing");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_Payments_Services");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Payments_AspNetUsers");
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Image).HasMaxLength(450);

                entity.Property(e => e.PetAddress).HasMaxLength(250);

                entity.Property(e => e.PetCity).HasMaxLength(50);

                entity.Property(e => e.PetName).HasMaxLength(100);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Allergy)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.AllergyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pets_Allergies");

                entity.HasOne(d => d.PetGender)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.PetGenderId)
                    .HasConstraintName("FK_Pets_Genders");

                entity.HasOne(d => d.PetType)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.PetTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pets_PetType");
            });

            modelBuilder.Entity<PetBreed>(entity =>
            {
                entity.ToTable("PetBreed");

                entity.HasIndex(e => e.PetTypeId, "IX_SubCategories_CategoryId");

                entity.Property(e => e.BreedName).HasMaxLength(100);

                entity.HasOne(d => d.PetType)
                    .WithMany(p => p.PetBreeds)
                    .HasForeignKey(d => d.PetTypeId)
                    .HasConstraintName("FK_PetBreed_PetType");
            });

            modelBuilder.Entity<PetType>(entity =>
            {
                entity.ToTable("PetType");

                entity.Property(e => e.PetType1)
                    .HasMaxLength(50)
                    .HasColumnName("PetType");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.ServiceName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
