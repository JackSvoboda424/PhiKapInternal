using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PhiKapInternal.Models.PKT_Internal;

namespace PhiKapInternal.Models.PKT_Internal2;

public partial class PktInternalContext : DbContext
{
    public PktInternalContext()
    {
    }

    public PktInternalContext(DbContextOptions<PktInternalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<StudyHourEntry> StudyHourEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = 40.122.162.212; Database=PKT_Internal; Trusted_Connection = False; TrustServerCertificate=True; User Id = sa; Password=Trains11!ns24; MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
