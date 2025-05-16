using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tutorial10.RestAPI;

public partial class MasterContext : DbContext
{

    public MasterContext()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departemnt> Departemnts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departemnt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departem__3214EC0766BD7A87");

            entity.ToTable("Departemnt");

            entity.HasIndex(e => e.Name, "UQ__Departem__737584F6985694C1").IsUnique();

            entity.Property(e => e.Location)
                .HasMaxLength(33)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0711B58C9B");

            entity.ToTable("Employee");

            entity.Property(e => e.Commission).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__Depart__45DE573A");

            entity.HasOne(d => d.Job).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__JobId__43F60EC8");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Employee__Manage__44EA3301");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Job__3214EC07430D8F9E");

            entity.ToTable("Job");

            entity.HasIndex(e => e.Name, "UQ__Job__737584F6DCF3FABD").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
