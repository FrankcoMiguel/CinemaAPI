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

        public virtual DbSet<AgeRating> AgeRating { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<FilmGenre> FilmGenre { get; set; }
        public virtual DbSet<FilmPerson> FilmPerson { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Occupation> Occupation { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonFilmOccupation> PersonFilmOccupation { get; set; }
        public virtual DbSet<PersonOccupation> PersonOccupation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgeRating>(entity =>
            {
                entity.ToTable("AgeRating", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("AgeRatingId");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ__AgeRatin__A25C5AA7475A8FAA")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__AgeRatin__737584F6D8F72F8E")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("Film", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("FilmId");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");

                entity.Property(e => e.Synopsis)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.AgeRating)
                    .WithMany(p => p.Film)
                    .HasForeignKey(d => d.AgeRatingId)
                    .HasConstraintName("FK__Film__AgeRatingI__078C1F06");
            });

            modelBuilder.Entity<FilmGenre>(entity =>
            {
                entity.ToTable("Film_Genre", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("FilmGenreId");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmGenre)
                    .HasForeignKey(d => d.FilmId)
                    .HasConstraintName("FK__Film_Genr__FilmI__16CE6296");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.FilmGenre)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__Film_Genr__Genre__17C286CF");
            });

            modelBuilder.Entity<FilmPerson>(entity =>
            {
                entity.ToTable("Film_Person", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("FilmPersonId");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmPerson)
                    .HasForeignKey(d => d.FilmId)
                    .HasConstraintName("FK__Film_Pers__FilmI__1A9EF37A");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.FilmPerson)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__Film_Pers__Perso__1B9317B3");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("GenreId");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(600)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Occupation>(entity =>
            {
                entity.ToTable("Occupation", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("OccupationId");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(600)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("PersonId");

                entity.Property(e => e.Description)
                    .IsRequired()
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
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PersonFilmOccupation>(entity =>
            {
                entity.ToTable("Person_Film_Occupation", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("PersonFilmOccupationId");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.PersonFilmOccupation)
                    .HasForeignKey(d => d.FilmId)
                    .HasConstraintName("FK__Person_Fi__FilmI__11158940");

                entity.HasOne(d => d.Occupation)
                    .WithMany(p => p.PersonFilmOccupation)
                    .HasForeignKey(d => d.OccupationId)
                    .HasConstraintName("FK__Person_Fi__Occup__1209AD79");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonFilmOccupation)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__Person_Fi__Perso__10216507");
            });

            modelBuilder.Entity<PersonOccupation>(entity =>
            {
                entity.ToTable("Person_Occupation", "Cinema");

                entity.Property(e => e.Id)
                .HasColumnName("PersonOccupationId");

                entity.HasOne(d => d.Occupation)
                    .WithMany(p => p.PersonOccupation)
                    .HasForeignKey(d => d.OccupationId)
                    .HasConstraintName("FK__Person_Oc__Occup__0D44F85C");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonOccupation)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__Person_Oc__Perso__0C50D423");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
