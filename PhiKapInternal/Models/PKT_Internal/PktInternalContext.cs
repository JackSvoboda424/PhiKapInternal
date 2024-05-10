using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PhiKapInternal.Models.PKT_Internal;

public partial class PktInternalContext : DbContext
{
    public PktInternalContext()
    {
    }

    public PktInternalContext(DbContextOptions<PktInternalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<StudyHourEntry> StudyHourEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = 40.122.162.212; Database=PKT_Internal; Trusted_Connection = False; TrustServerCertificate=True; User Id = sa; Password=Trains11!ns24; MultipleActiveResultSets=True");
    /// <summary>
    /// deploy: Server = 10.1.0.4;
    /// dev: Server = 40.122.162.212;
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Class)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("class");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("position");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Room).HasColumnName("room");

        });

        modelBuilder.Entity<StudyHourEntry>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssociateId).HasColumnName("associate_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.ProctorId).HasColumnName("proctor_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
