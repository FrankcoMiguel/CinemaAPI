using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CinemaAPI.Core.Entities;

namespace CinemaAPI.Infrastructure.Data
{
    public partial class CinemaAPIContext : DbContext
    {
        public CinemaAPIContext()
        {
        }

        public CinemaAPIContext(DbContextOptions<CinemaAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Crew> Crew { get; set; }
        public virtual DbSet<CrewMember> CrewMember { get; set; }
        public virtual DbSet<CrewRole> CrewRole { get; set; }
        public virtual DbSet<Director> Director { get; set; }
        public virtual DbSet<Film> Film { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(e => e.Id)
                .HasColumnName("ActorId")
                .ValueGeneratedNever();

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Image)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");
            });

            modelBuilder.Entity<Crew>(entity =>
            {
                entity.Property(e => e.Id)
                .HasColumnName("CrewId")
                .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CrewMember>(entity =>
            {
                entity.Property(e => e.Id)
                .HasColumnName("CrewMemberId")
                .ValueGeneratedNever();

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Image)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Crew)
                    .WithMany(p => p.CrewMember)
                    .HasForeignKey(d => d.CrewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Crew");

                entity.HasOne(d => d.CrewRole)
                    .WithMany(p => p.CrewMember)
                    .HasForeignKey(d => d.CrewRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Role");
            });

            modelBuilder.Entity<CrewRole>(entity =>
            {
                entity.Property(e => e.Id)
                .HasColumnName("CrewRoleId")
                .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Director>(entity =>
            {
                entity.Property(e => e.Id)
                .HasColumnName("DirectorId")
                .ValueGeneratedNever();

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Image)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.Property(e => e.Id)
                .HasColumnName("FilmId")
                .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Director)
                    .WithMany(p => p.Film)
                    .HasForeignKey(d => d.DirectorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Film_Director");
            });

        }
    }
}
