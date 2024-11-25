using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DisplayDeletedMessages.Models;

public partial class BotAdministratorContext : DbContext
{
    public BotAdministratorContext()
    {
    }

    public BotAdministratorContext(DbContextOptions<BotAdministratorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Datejoined> Datejoineds { get; set; }

    public virtual DbSet<Deletedmessage> Deletedmessages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BotAdministrator;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Datejoined>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("datejoined_pkey");

            entity.ToTable("datejoined");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Datetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datetime");
        });

        modelBuilder.Entity<Deletedmessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("deletedmessages_pkey");

            entity.ToTable("deletedmessages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DatetimeOfMessageSending)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datetime_of_message_sending");
            entity.Property(e => e.MessageOrPath)
                .HasColumnType("character varying")
                .HasColumnName("message_or_path");
            entity.Property(e => e.ReasonForDeleted)
                .HasMaxLength(120)
                .HasColumnName("reason_for_deleted");
            entity.Property(e => e.TypeMessage)
                .HasMaxLength(5)
                .HasColumnName("type_message");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Deletedmessages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deletedmessages_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
            entity.Property(e => e.Username)
                .HasMaxLength(70)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
