using System;
using System.Collections.Generic;
using KioscoInformaticoBackend.Enums;
using KioscoInformaticoBackend.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace KioscoInformaticoBackend.DataContext;

public partial class KioscoContext : DbContext
{
    public KioscoContext()
    {
    }

    public KioscoContext(DbContextOptions<KioscoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Detallescompra> Detallescompras { get; set; }

    public virtual DbSet<Detallesventa> Detallesventas { get; set; }


    public virtual DbSet<Localidad> Localidades { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedores { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        string? cadenaConexion = configuration.GetConnectionString("mysqlRemoto");

        optionsBuilder.UseMySql(cadenaConexion, ServerVersion.AutoDetect(cadenaConexion));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.HasIndex(e => e.LocalidadId, "FK_Clientes_Localidades_LocalidadId");

            entity.Property(e => e.FechaNacimiento).HasMaxLength(6);

            entity.HasOne(d => d.Localidad).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.LocalidadId)
                .HasConstraintName("FK_Clientes_Localidades_LocalidadId");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("compras");

            entity.HasIndex(e => e.ProveedorId, "FK_Compras_Proveedores_ProveedorID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Fecha).HasMaxLength(6);
            entity.Property(e => e.ProveedorId).HasColumnName("ProveedorID");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Compras)
                .HasForeignKey(d => d.ProveedorId)
                .HasConstraintName("FK_Compras_Proveedores_ProveedorID");
        });

        modelBuilder.Entity<Detallescompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detallescompras");

            entity.HasIndex(e => e.CompraId, "FK_DetallesCompras_Compras_CompraId");

            entity.HasIndex(e => e.ProductoId, "FK_DetallesCompras_Productos_ProductoId");

            entity.HasOne(d => d.Compra).WithMany(p => p.Detallescompras)
                .HasForeignKey(d => d.CompraId)
                .HasConstraintName("FK_DetallesCompras_Compras_CompraId");

            entity.HasOne(d => d.Producto).WithMany(p => p.Detallescompras)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK_DetallesCompras_Productos_ProductoId");
        });

        modelBuilder.Entity<Detallesventa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detallesventas");

            entity.HasIndex(e => e.ProductoId, "FK_DetallesVentas_Productos_ProductoId");

            entity.HasIndex(e => e.VentaId, "FK_DetallesVentas_Ventas_VentaId");

            entity.HasOne(d => d.Producto).WithMany(p => p.Detallesventa)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK_DetallesVentas_Productos_ProductoId");

            entity.HasOne(d => d.Venta).WithMany(p => p.Detallesventa)
                .HasForeignKey(d => d.VentaId)
                .HasConstraintName("FK_DetallesVentas_Ventas_VentaId");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("localidades");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("productos");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proveedores");

            entity.HasIndex(e => e.LocalidadId, "FK_Proveedores_Localidades_LocalidadId");

            entity.HasOne(d => d.Localidad).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.LocalidadId)
                .HasConstraintName("FK_Proveedores_Localidades_LocalidadId");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ventas");

            entity.HasIndex(e => e.ClienteId, "FK_Ventas_Clientes_ClienteId");

            entity.Property(e => e.Fecha).HasMaxLength(6);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Venta)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_Ventas_Clientes_ClienteId");
        });
        #region
        // Carga de datos semilla de Productos
        modelBuilder.Entity<Producto>().HasData(
            new Producto() { Id = 1, Nombre = "Coca-Cola 2lts", Precio = 2500 },
            new Producto() { Id = 2, Nombre = "Papas Lays 160grs", Precio = 1500 },
            new Producto() { Id = 3, Nombre = "Agua Mineral 2lts", Precio = 2000 }
            );

        // Carga de datos semilla de Localidades
        modelBuilder.Entity<Localidad>().HasData(
            new Localidad() { Id = 1, Nombre = "San Justo" },
            new Localidad() { Id = 2, Nombre = "Videla" },
            new Localidad() { Id = 3, Nombre = "Reconquista" }
            );

        // Carga de datos semilla de Clientes
        modelBuilder.Entity<Cliente>().HasData(
            new Cliente
            {
                Id = 1,
                Nombre = "Juan Pérez",
                Direccion = "Calle Falsa 123",
                Telefonos = "123456789",
                FechaNacimiento = new DateTime(1985, 5, 15),
                LocalidadId = 1
            },
            new Cliente
            {
                Id = 2,
                Nombre = "María López",
                Direccion = "Avenida Siempre Viva 742",
                Telefonos = "987654321",
                FechaNacimiento = new DateTime(1990, 8, 25),
                LocalidadId = 2
            },
            new Cliente
            {
                Id = 3,
                Nombre = "Carlos García",
                Direccion = "Boulevard de los Sueños Rotos 101",
                Telefonos = "555666777",
                FechaNacimiento = new DateTime(1978, 2, 3),
                LocalidadId = 3
            },
            new Cliente
            {
                Id = 4,
                Nombre = "Ana Martínez",
                Direccion = "Ruta Nacional 19 Km 58",
                Telefonos = "444555666",
                FechaNacimiento = new DateTime(2000, 12, 12),
                LocalidadId = 1
            }
            );

        // Carga de datos semilla de Proveedores
        modelBuilder.Entity<Proveedor>().HasData(
        new Proveedor
        {
            Id = 1,
            Nombre = "Proveedor A",
            Direccion = "Calle 1",
            Telefonos = "111111111",
            Cbu = "0000003100010000000001",
            CondicionIvaEnum = CondicionIvaEnum.ResponsableInscripto,
            LocalidadId = 1
        },
        new Proveedor
        {
            Id = 2,
            Nombre = "Proveedor B",
            Direccion = "Calle 2",
            Telefonos = "222222222",
            Cbu = "0000003100010000000002",
            CondicionIvaEnum = CondicionIvaEnum.Monotributista,
            LocalidadId = 2
        },
        new Proveedor
        {
            Id = 3,
            Nombre = "Proveedor C",
            Direccion = "Calle 3",
            Telefonos = "333333333",
            Cbu = "0000003100010000000003",
            CondicionIvaEnum = CondicionIvaEnum.ConsumidorFinal,
            LocalidadId = 3
        },
        new Proveedor
        {
            Id = 4,
            Nombre = "Proveedor D",
            Direccion = "Calle 4",
            Telefonos = "444444444",
            Cbu = "0000003100010000000004",
            CondicionIvaEnum = CondicionIvaEnum.Exento,
            LocalidadId = 1
        },
        new Proveedor
        {
            Id = 5,
            Nombre = "Proveedor E",
            Direccion = "Calle 5",
            Telefonos = "555555555",
            Cbu = "0000003100010000000005",
            CondicionIvaEnum = CondicionIvaEnum.NoResponsable,
            LocalidadId = 1
        },
        new Proveedor
        {
            Id = 6,
            Nombre = "Proveedor F",
            Direccion = "Calle 6",
            Telefonos = "666666666",
            Cbu = "0000003100010000000006",
            CondicionIvaEnum = CondicionIvaEnum.ResponsableNoInscripto,
            LocalidadId = 2
        },
        new Proveedor
        {
            Id = 7,
            Nombre = "Proveedor G",
            Direccion = "Calle 7",
            Telefonos = "777777777",
            Cbu = "0000003100010000000007",
            CondicionIvaEnum = CondicionIvaEnum.ResponsableInscripto,
            LocalidadId = 3
        },
        new Proveedor
        {
            Id = 8,
            Nombre = "Proveedor H",
            Direccion = "Calle 8",
            Telefonos = "888888888",
            Cbu = "0000003100010000000008",
            CondicionIvaEnum = CondicionIvaEnum.SujetoNoCategorizado,
            LocalidadId = 2
        },
        new Proveedor
        {
            Id = 9,
            Nombre = "Proveedor I",
            Direccion = "Calle 9",
            Telefonos = "999999999",
            Cbu = "0000003100010000000009",
            CondicionIvaEnum = CondicionIvaEnum.Monotributista,
            LocalidadId = 3
        },
        new Proveedor
        {
            Id = 10,
            Nombre = "Proveedor J",
            Direccion = "Calle 10",
            Telefonos = "101010101",
            Cbu = "0000003100010000000010",
            CondicionIvaEnum = CondicionIvaEnum.Exento,
            LocalidadId = 3
        }
    );
        // Carga de datos semilla de Ventas
        modelBuilder.Entity<Venta>().HasData(
            new Venta()
            {
                Id = 1,
                FormaPago = FormaDePagoEnum.Efectivo,
                Iva = 21m,
                Total = 3000m,
                ClienteId = 1,
                Fecha = DateTime.Now
            },
            new Venta()
            {
                Id = 2,
                FormaPago = FormaDePagoEnum.Tarjeta_Credito,
                Iva = 10,
                Total = 5000m,
                ClienteId = 2,
                Fecha = DateTime.Now
            },
            new Venta()
            {
                Id = 3,
                FormaPago = FormaDePagoEnum.Tarjeta_Debito,
                Iva = 21,
                Total = 8000m,
                ClienteId = 1,
                Fecha = DateTime.Now
            }
        );

        // Carga de datos semilla de Compras
        modelBuilder.Entity<Compra>().HasData(
            new Compra
            {
                Id = 1,
                FormaDePago = FormaDePagoEnum.Efectivo,
                Iva = 21,
                Total = 1000,
                Fecha = new DateTime(2021, 5, 15),
                ProveedorId = 1
            },
            new Compra
            {
                Id = 2,
                FormaDePago = FormaDePagoEnum.Tarjeta_Credito,
                Iva = 10,
                Total = 2000,
                Fecha = new DateTime(2021, 5, 16),
                ProveedorId = 2
            },
            new Compra
            {
                Id = 3,
                FormaDePago = FormaDePagoEnum.Tarjeta_Debito,
                Iva = 5,
                Total = 3000,
                Fecha = new DateTime(2021, 5, 17),
                ProveedorId = 3
            },
            new Compra
            {
                Id = 4,
                FormaDePago = FormaDePagoEnum.Efectivo,
                Iva = 0,
                Total = 4000,
                Fecha = new DateTime(2021, 5, 18),
                ProveedorId = 4
            }
        );

        // Carga de datos semilla de DetallesCompras
        modelBuilder.Entity<Detallescompra>().HasData(
        new Detallescompra { Id = 1, ProductoId = 1, PrecioUnitario = 2650, Cantidad = 1, CompraId = 1 },
        new Detallescompra { Id = 2, ProductoId = 2, PrecioUnitario = 2450, Cantidad = 2, CompraId = 2 },
        new Detallescompra { Id = 3, ProductoId = 3, PrecioUnitario = 2550, Cantidad = 1, CompraId = 3 }
        );

        // Carga de datos semilla de DetallesVentas
        modelBuilder.Entity<Detallesventa>().HasData(
            new Detallesventa { Id = 1, VentaId = 1, ProductoId = 1, Cantidad = 1, PrecioUnitario = 2650 },
            new Detallesventa { Id = 2, VentaId = 2, ProductoId = 2, Cantidad = 2, PrecioUnitario = 2450 },
            new Detallesventa { Id = 3, VentaId = 3, ProductoId = 3, Cantidad = 1, PrecioUnitario = 2550 }
            );
        #endregion

        OnModelCreatingPartial(modelBuilder);


    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
