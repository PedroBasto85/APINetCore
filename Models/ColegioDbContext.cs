using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIColegio.Models
{
    public class ColegioDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=CEO-PC; Initial Catalog=colegio; User=sa; Password=admin2021");
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<AlumnoGrado> Grupos { get; set; }

        protected override void OnModelCreating(ModelBuilder oModelBuilder)
        {
            oModelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.AlumnoID);
                entity.ToTable("alumno");
                entity.Property(e => e.AlumnoID).HasColumnName("AlumnoID");
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombre");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Apellidos");

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Genero");

                entity.Property(e => e.FechaNac)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("FechaNac");

                entity.Property(e => e.UM)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasColumnName("UM");
            });

            oModelBuilder.Entity<Profesor>(entity =>
            {
                entity.HasKey(e => e.ProfesorID);
                entity.ToTable("profesor");
                entity.Property(e => e.ProfesorID).HasColumnName("ProfesorID");
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombre");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Apellidos");

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Genero");

                entity.Property(e => e.UM)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasColumnName("UM");

            });

            oModelBuilder.Entity<Grado>(entity =>
            {
                entity.HasKey(e => e.GradoID);
                entity.ToTable("grado");
            
            });

            oModelBuilder.Entity<AlumnoGrado>(entity =>
            {
                entity.HasKey(e => e.GrupoID);
                entity.ToTable("alumnogrado");

            });
           

        }

    }
}

 




