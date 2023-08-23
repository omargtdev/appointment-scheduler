using AppointmentScheduler.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentScheduler.Database;

public partial class AppointmentSchedulerContext : DbContext
{
    public AppointmentSchedulerContext()
    {
    }

    public AppointmentSchedulerContext(DbContextOptions<AppointmentSchedulerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentState> AppointmentStates { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA240A6909C");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.AppointmentContactId).HasColumnName("AppointmentContactID");
            entity.Property(e => e.AppointmentStateId).HasColumnName("AppointmentStateID");
            entity.Property(e => e.AppointmentUserId).HasColumnName("AppointmentUserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.Subject)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.AppointmentContact).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.AppointmentContactId)
                .HasConstraintName("FK_Appointment_ContactID");

            entity.HasOne(d => d.AppointmentState).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.AppointmentStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointment_AppointmentStateID");

            entity.HasOne(d => d.AppointmentUser).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.AppointmentUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointment_UserID");
        });

        modelBuilder.Entity<AppointmentState>(entity =>
        {
            entity.HasKey(e => e.AppointmentStateId).HasName("PK__Appointm__F637773A755D45DF");

            entity.ToTable("AppointmentState");

            entity.Property(e => e.AppointmentStateId)
                .ValueGeneratedNever()
                .HasColumnName("AppointmentStateID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__Contact__5C6625BB8BCFB335");

            entity.ToTable("Contact");

            entity.Property(e => e.ContactId).HasColumnName("ContactID");
            entity.Property(e => e.ContactUserId).HasColumnName("ContactUserID");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.ContactUser).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.ContactUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_UserID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACB85273FF");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
