using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace microTrading.Models;

public partial class MicroTradingContext : DbContext
{
    public MicroTradingContext()
    {
    }

    public MicroTradingContext(DbContextOptions<MicroTradingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Active> Actives { get; set; }

    public virtual DbSet<RunPerfomance> RunPerfomances { get; set; }

    public virtual DbSet<ValueRecord> ValueRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Active>(entity =>
        {
            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Symbol)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("symbol");
        });

        modelBuilder.Entity<RunPerfomance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Run_Perf__3214EC077A0AA797");

            entity.ToTable("Run_Perfomances");

            entity.Property(e => e.IdActive).HasColumnName("idActive");
            entity.Property(e => e.RunStart).HasColumnType("datetime");
            entity.Property(e => e.RunStop).HasColumnType("datetime");

            entity.HasOne(d => d.IdActiveNavigation).WithMany(p => p.RunPerfomances)
                .HasForeignKey(d => d.IdActive)
                .HasConstraintName("FK_Run_Perfomances_ToActives");
        });

        modelBuilder.Entity<ValueRecord>(entity =>
        {
            entity.ToTable("value_records");

            entity.HasIndex(e => e.ActiveId, "Symbol_Index");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.RecordDate)
                .HasColumnType("datetime")
                .HasColumnName("record_date");
            entity.Property(e => e.RunId).HasColumnName("runId");

            entity.HasOne(d => d.Active).WithMany(p => p.ValueRecords)
                .HasForeignKey(d => d.ActiveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_value_records_ToActives");

            entity.HasOne(d => d.Run).WithMany(p => p.ValueRecords)
                .HasForeignKey(d => d.RunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Value_Records_ToRun_Performances");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
