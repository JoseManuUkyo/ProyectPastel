using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Pastel.Entities;

public partial class proyecto_pastelContext : DbContext
{
    public proyecto_pastelContext()
    {
    }

    public proyecto_pastelContext(DbContextOptions<proyecto_pastelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<detalle_venta> detalle_venta { get; set; }

    public virtual DbSet<ingredientes_receta> ingredientes_receta { get; set; }

    public virtual DbSet<inventario> inventario { get; set; }

    public virtual DbSet<movimientos_inventario> movimientos_inventario { get; set; }

    public virtual DbSet<postres> postres { get; set; }

    public virtual DbSet<recetas> recetas { get; set; }

    public virtual DbSet<usuarios> usuarios { get; set; }

    public virtual DbSet<ventas> ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;uid=root;pwd=Camila2015;database=proyecto_pastel");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<detalle_venta>(entity =>
        {
            entity.HasKey(e => e.id_detalle).HasName("PRIMARY");

            entity.HasIndex(e => e.id_postre, "id_postre");

            entity.HasIndex(e => e.id_venta, "id_venta");

            entity.Property(e => e.precio_unitario).HasPrecision(10);
            entity.Property(e => e.subtotal).HasPrecision(10);

            entity.HasOne(d => d.id_postreNavigation).WithMany(p => p.detalle_venta)
                .HasForeignKey(d => d.id_postre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalle_venta_ibfk_2");

            entity.HasOne(d => d.id_ventaNavigation).WithMany(p => p.detalle_venta)
                .HasForeignKey(d => d.id_venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalle_venta_ibfk_1");
        });

        modelBuilder.Entity<ingredientes_receta>(entity =>
        {
            entity.HasKey(e => e.id_ingrediente_receta).HasName("PRIMARY");

            entity.HasIndex(e => e.id_ingrediente, "id_ingrediente");

            entity.HasIndex(e => e.id_receta, "id_receta");

            entity.Property(e => e.cantidad).HasPrecision(10);
            entity.Property(e => e.unidad).HasColumnType("enum('g','kg','ml','l','unidad')");

            entity.HasOne(d => d.id_ingredienteNavigation).WithMany(p => p.ingredientes_receta)
                .HasForeignKey(d => d.id_ingrediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredientes_receta_ibfk_2");

            entity.HasOne(d => d.id_recetaNavigation).WithMany(p => p.ingredientes_receta)
                .HasForeignKey(d => d.id_receta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredientes_receta_ibfk_1");
        });

        modelBuilder.Entity<inventario>(entity =>
        {
            entity.HasKey(e => e.id_ingrediente).HasName("PRIMARY");

            entity.Property(e => e.cantidad).HasPrecision(10);
            entity.Property(e => e.fecha_actualizacion)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.unidad).HasColumnType("enum('g','kg','ml','l','unidad')");
        });

        modelBuilder.Entity<movimientos_inventario>(entity =>
        {
            entity.HasKey(e => e.id_movimiento).HasName("PRIMARY");

            entity.HasIndex(e => e.id_ingrediente, "id_ingrediente");

            entity.HasIndex(e => e.id_usuario, "id_usuario");

            entity.Property(e => e.cantidad).HasPrecision(10);
            entity.Property(e => e.descripcion).HasColumnType("text");
            entity.Property(e => e.fecha_movimiento)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.motivo).HasColumnType("enum('ajuste','descompuesto','producción','otro')");
            entity.Property(e => e.tipo_movimiento).HasColumnType("enum('entrada','salida')");

            entity.HasOne(d => d.id_ingredienteNavigation).WithMany(p => p.movimientos_inventario)
                .HasForeignKey(d => d.id_ingrediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("movimientos_inventario_ibfk_1");

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.movimientos_inventario)
                .HasForeignKey(d => d.id_usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("movimientos_inventario_ibfk_2");
        });

        modelBuilder.Entity<postres>(entity =>
        {
            entity.HasKey(e => e.id_postre).HasName("PRIMARY");

            entity.Property(e => e.descripcion).HasColumnType("text");
            entity.Property(e => e.fecha_creacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.precio_base).HasPrecision(10);
        });

        modelBuilder.Entity<recetas>(entity =>
        {
            entity.HasKey(e => e.id_receta).HasName("PRIMARY");

            entity.HasIndex(e => e.id_postre, "id_postre");

            entity.Property(e => e.descripcion).HasColumnType("text");

            entity.HasOne(d => d.id_postreNavigation).WithMany(p => p.recetas)
                .HasForeignKey(d => d.id_postre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recetas_ibfk_1");
        });

        modelBuilder.Entity<usuarios>(entity =>
        {
            entity.HasKey(e => e.id_usuario).HasName("PRIMARY");

            entity.HasIndex(e => e.correo, "correo").IsUnique();

            entity.Property(e => e.contraseña).HasMaxLength(255);
            entity.Property(e => e.correo).HasMaxLength(100);
            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<ventas>(entity =>
        {
            entity.HasKey(e => e.id_venta).HasName("PRIMARY");

            entity.HasIndex(e => e.id_usuario, "id_usuario");

            entity.Property(e => e.fecha_venta)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.ventas)
                .HasForeignKey(d => d.id_usuario)
                .HasConstraintName("ventas_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
