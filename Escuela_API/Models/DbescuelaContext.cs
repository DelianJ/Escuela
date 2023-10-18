using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Escuela_API.Models;

public partial class DbescuelaContext : DbContext
{
    public DbescuelaContext()
    {
    }

    public DbescuelaContext(DbContextOptions<DbescuelaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<MateriasEstudiante> MateriasEstudiantes { get; set; }

    public virtual DbSet<MateriasProfesore> MateriasProfesores { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-N2IQ2ON0;Initial Catalog=DBEscuela;Trusted_Connection=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__Estudian__9B410A8921DF7E22");

            entity.Property(e => e.IdEstudiante).HasColumnName("ID_Estudiante");
            entity.Property(e => e.NombreEstudiante)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Estudiante");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PK__Materias__4BAC7BD9CDB6CDC9");

            entity.Property(e => e.IdMateria).HasColumnName("ID_Materia");
            entity.Property(e => e.NomnbreMateria)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Nomnbre_Materia");
        });

        modelBuilder.Entity<MateriasEstudiante>(entity =>
        {
            entity.HasKey(e => e.IdMateriasEstudiantes).HasName("PK__Materias__7BCD99826EB2C6B9");

            entity.ToTable("Materias_Estudiantes");

            entity.Property(e => e.IdMateriasEstudiantes).HasColumnName("ID_MateriasEstudiantes");
            entity.Property(e => e.Calificacion).HasColumnType("decimal(3, 1)");
            entity.Property(e => e.IdEstudiante).HasColumnName("ID_Estudiante");
            entity.Property(e => e.IdMateria).HasColumnName("ID_Materia");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.MateriasEstudiantes)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Materias___ID_Es__60A75C0F");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.MateriasEstudiantes)
                .HasForeignKey(d => d.IdMateria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Materias___ID_Ma__619B8048");
        });

        modelBuilder.Entity<MateriasProfesore>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Materias_Profesores");

            entity.HasIndex(e => e.IdMateria, "UQ__Materias__4BAC7BD8D11CD16F").IsUnique();

            entity.Property(e => e.IdMateria).HasColumnName("ID_Materia");
            entity.Property(e => e.IdProfesor).HasColumnName("ID_Profesor");

            entity.HasOne(d => d.IdMateriaNavigation).WithOne()
                .HasForeignKey<MateriasProfesore>(d => d.IdMateria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Materias___ID_Ma__4E88ABD4");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany()
                .HasForeignKey(d => d.IdProfesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Materias___ID_Pr__4D94879B");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.IdProfesor).HasName("PK__Profesor__4D3751F6DC299FE4");

            entity.Property(e => e.IdProfesor).HasColumnName("ID_Profesor");
            entity.Property(e => e.NombreProfesor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Profesor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
