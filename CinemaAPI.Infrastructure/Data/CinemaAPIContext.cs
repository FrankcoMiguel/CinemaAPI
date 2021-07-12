using CinemaAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<Casting> Casting { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Occupation> Occupation { get; set; }
        public virtual DbSet<Occupations> Occupations { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Casting>(entity =>
            {
                entity.ToTable("Casting", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("CastingId");

                entity.HasIndex(e => new { e.PersonId, e.FilmId, e.OccupationId })
                    .HasName("UQ_Casting")
                    .IsUnique();

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Casting)
                    .HasForeignKey(d => d.FilmId)
                    .HasConstraintName("FK__Casting__FilmId__0EF836A4");

                entity.HasOne(d => d.Occupation)
                    .WithMany(p => p.Casting)
                    .HasForeignKey(d => d.OccupationId)
                    .HasConstraintName("FK__Casting__Occupat__0FEC5ADD");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Casting)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__Casting__PersonI__0E04126B");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("Film", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("FilmId");


                entity.HasIndex(e => e.Title)
                    .HasName("UQ_Film")
                    .IsUnique();

                entity.Property(e => e.BoxOffice).HasColumnType("decimal(20, 1)");

                entity.Property(e => e.Budget).HasColumnType("decimal(20, 1)");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PictureReference)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Score).HasColumnType("decimal(2, 1)");

                entity.Property(e => e.Synopsis)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Rating)
                    .WithMany(p => p.Film)
                    .HasForeignKey(d => d.RatingId)
                    .HasConstraintName("FK__Film__RatingId__084B3915");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("GenreId");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_Genre")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(600)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.ToTable("Genres", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("GenresId");

                entity.HasIndex(e => new { e.FilmId, e.GenreId })
                    .HasName("UQ_Genres")
                    .IsUnique();

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Genres)
                    .HasForeignKey(d => d.FilmId)
                    .HasConstraintName("FK__Genres__FilmId__13BCEBC1");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Genres)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__Genres__GenreId__14B10FFA");
            });

            modelBuilder.Entity<Occupation>(entity =>
            {
                entity.ToTable("Occupation", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("OccupationId");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_Occupation")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(600)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Occupations>(entity =>
            {
                entity.ToTable("Occupations", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("OccupationsId");

                entity.HasIndex(e => new { e.PersonId, e.OccupationId })
                    .HasName("UQ_Occupations")
                    .IsUnique();

                entity.HasOne(d => d.Occupation)
                    .WithMany(p => p.Occupations)
                    .HasForeignKey(d => d.OccupationId)
                    .HasConstraintName("FK__Occupatio__Occup__7DCDAAA2");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Occupations)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__Occupatio__Perso__7CD98669");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("PersonId");

                entity.HasIndex(e => new { e.Firstname, e.Lastname, e.Age })
                    .HasName("UQ_Person")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PictureReference)
                    .HasMaxLength(4000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("RatingId");

                entity.HasIndex(e => new { e.Code, e.Name })
                    .HasName("UQ_Rating")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
