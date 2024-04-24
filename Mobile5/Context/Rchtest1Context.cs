using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Mobile5.Models;

namespace Mobile5.Context;

public partial class Rchtest1Context : DbContext
{
    public Rchtest1Context()
    {
    }

    public Rchtest1Context(DbContextOptions<Rchtest1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<TypeCargo> TypeCargos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost:5432;Database=rchtest1;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("city_pkey");

            entity.ToTable("city");

            entity.Property(e => e.CityId).HasColumnName("cityID");
            entity.Property(e => e.CityName)
                .HasColumnType("character varying")
                .HasColumnName("cityName");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("company_pkey");

            entity.ToTable("company");

            entity.Property(e => e.CompanyId).HasColumnName("companyID");
            entity.Property(e => e.CompanyName)
                .HasColumnType("character varying")
                .HasColumnName("companyName");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.EmployeeId).HasColumnName("employeeID");
            entity.Property(e => e.EMail)
                .HasColumnType("character varying")
                .HasColumnName("e-mail");
            entity.Property(e => e.LastName)
                .HasColumnType("character varying")
                .HasColumnName("lastName");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Patronimyc)
                .HasColumnType("character varying")
                .HasColumnName("patronimyc");
            entity.Property(e => e.PositionId).HasColumnName("positionID");
            entity.Property(e => e.TelephoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("telephoneNumber");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("positionID");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("position_pkey");

            entity.ToTable("position");

            entity.Property(e => e.PositionId).HasColumnName("positionID");
            entity.Property(e => e.PositionName)
                .HasColumnType("character varying")
                .HasColumnName("positionName");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.TransportId).HasName("transport_pkey");

            entity.ToTable("transport");

            entity.Property(e => e.TransportId).HasColumnName("transportID");
            entity.Property(e => e.ArrivalCity).HasColumnName("arrivalCity");
            entity.Property(e => e.CompanyId).HasColumnName("companyID");
            entity.Property(e => e.DepartureCity).HasColumnName("departureCity");
            entity.Property(e => e.EmployeeId).HasColumnName("employeeID");
            entity.Property(e => e.TypeCargoId).HasColumnName("typeCargoID");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.ArrivalCityNavigation).WithMany(p => p.TransportArrivalCityNavigations)
                .HasForeignKey(d => d.ArrivalCity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("arrivalCity");

            entity.HasOne(d => d.Company).WithMany(p => p.Transports)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("companyID");

            entity.HasOne(d => d.DepartureCityNavigation).WithMany(p => p.TransportDepartureCityNavigations)
                .HasForeignKey(d => d.DepartureCity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("departureCity");

            entity.HasOne(d => d.Employee).WithMany(p => p.Transports)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employeeID");

            entity.HasOne(d => d.TypeCargo).WithMany(p => p.Transports)
                .HasForeignKey(d => d.TypeCargoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("typeCargoID");
        });

        modelBuilder.Entity<TypeCargo>(entity =>
        {
            entity.HasKey(e => e.TypeCargoId).HasName("typeCargo_pkey");

            entity.ToTable("typeCargo");

            entity.Property(e => e.TypeCargoId).HasColumnName("typeCargoID");
            entity.Property(e => e.TypeCargoName)
                .HasMaxLength(50)
                .HasColumnName("typeCargoName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
