using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_API.Data;

public partial class BibliotecaContext : DbContext
{
    public BibliotecaContext()
    {
    }

    public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=SANTIAGO\\SQLEXPRESS;Database=biblioteca;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__libros__3213E83F8B0DBD26");

            entity.ToTable("libros");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnoPublicacion).HasColumnName("anoPublicacion");
            entity.Property(e => e.Autor)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("autor");
            entity.Property(e => e.Genero)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("genero");
            entity.Property(e => e.Titulo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__prestamo__3213E83F8EFF6264");

            entity.ToTable("prestamos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaDevolucion)
                .HasColumnType("datetime")
                .HasColumnName("fechaDevolucion");
            entity.Property(e => e.FechaPrestamo)
                .HasColumnType("datetime")
                .HasColumnName("fechaPrestamo");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Idlibro).HasColumnName("idlibro");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__prestamos__fecha__3C69FB99");

            entity.HasOne(d => d.IdlibroNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.Idlibro)
                .HasConstraintName("FK__prestamos__idlib__3D5E1FD2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83F0E284F3D");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Cedula, "UQ__usuarios__415B7BE523D67133").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cedula).HasColumnName("cedula");
            entity.Property(e => e.CorreElectronico)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("correElectronico");
            entity.Property(e => e.NumeroContacto)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("numeroContacto");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primerApellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primerNombre");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("segundoApellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("segundoNombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
