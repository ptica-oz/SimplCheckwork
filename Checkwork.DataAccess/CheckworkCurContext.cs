using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Checkwork.DataAccess;

public partial class CheckworkCurContext : DbContext
{
    public CheckworkCurContext()
        :base()
    {
        Database.EnsureCreated();
    }

    public CheckworkCurContext(DbContextOptions<CheckworkCurContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Departament> Departaments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeFault> EmployeeFaults { get; set; }

    public virtual DbSet<EmployeeProperty> EmployeeProperties { get; set; }

    public virtual DbSet<EmployeeStatus> EmployeeStatuses { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<MonthlyWorkHour> MonthlyWorkHours { get; set; }

    public virtual DbSet<OffType> OffTypes { get; set; }

    public virtual DbSet<OffWorkSheet> OffWorkSheets { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Smkdocument> Smkdocuments { get; set; }

    public virtual DbSet<Smkdocument2DepartamentPost> Smkdocument2DepartamentPosts { get; set; }

    public virtual DbSet<Smkdocument2Employee> Smkdocument2Employees { get; set; }

    public virtual DbSet<Smkdocument2Post> Smkdocument2Posts { get; set; }

    public virtual DbSet<WorkEvent> WorkEvents { get; set; }

    public virtual DbSet<WorkSheet> WorkSheets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.CalendarDate);

            entity.ToTable("Calendar");

            entity.Property(e => e.CalendarDate).HasColumnType("datetime");
            entity.Property(e => e.WorkHours).HasDefaultValue(9);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Departament>(entity =>
        {
            entity.ToTable("Departament");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.ParentDepartament).WithMany(p => p.InverseParentDepartament)
                .HasForeignKey(d => d.ParentDepartamentId)
                .HasConstraintName("FK_Departament_Departament");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Idemployee).HasFillFactor(90);

            entity.ToTable("Employee");

            entity.Property(e => e.Idemployee).HasColumnName("IDEmployee");
            entity.Property(e => e.IdemployeeStatus).HasColumnName("IDEmployeeStatus");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Patronimyc)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdemployeeStatusNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdemployeeStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_EmployeeStatus");
        });

        modelBuilder.Entity<EmployeeFault>(entity =>
        {
            entity.HasKey(e => e.IdemployeeFault).HasName("PK_EmployeeFault1");

            entity.ToTable("EmployeeFault");

            entity.Property(e => e.IdemployeeFault).HasColumnName("IDEmployeeFault");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Idemployee).HasColumnName("IDEmployee");

            entity.HasOne(d => d.IdemployeeNavigation).WithMany(p => p.EmployeeFaults)
                .HasForeignKey(d => d.Idemployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeFault_Employee");
        });

        modelBuilder.Entity<EmployeeProperty>(entity =>
        {
            entity.HasKey(e => e.EmployeePropertiesId);

            entity.Property(e => e.Education)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MobilePhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Passport)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Picture).HasColumnType("image");
            entity.Property(e => e.Pnumber)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("PNumber");
            entity.Property(e => e.Skype)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.EmployeeProperties)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_EmployeeProperties_Category");

            entity.HasOne(d => d.Departament).WithMany(p => p.EmployeeProperties)
                .HasForeignKey(d => d.DepartamentId)
                .HasConstraintName("FK_EmployeeProperties_Departament");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeePropertyEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeProperties_Employee");

            entity.HasOne(d => d.Location).WithMany(p => p.EmployeeProperties)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_EmployeeProperties_Location");

            entity.HasOne(d => d.MasterEmployee).WithMany(p => p.EmployeePropertyMasterEmployees)
                .HasForeignKey(d => d.MasterEmployeeId)
                .HasConstraintName("FK_EmployeeProperties_Master");

            entity.HasOne(d => d.Post).WithMany(p => p.EmployeeProperties)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_EmployeeProperties_Post");
        });

        modelBuilder.Entity<EmployeeStatus>(entity =>
        {
            entity.HasKey(e => e.IdemployeeStatus);

            entity.ToTable("EmployeeStatus");

            entity.Property(e => e.IdemployeeStatus).HasColumnName("IDEmployeeStatus");
            entity.Property(e => e.Code)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.IdeventType).HasFillFactor(90);

            entity.ToTable("EventType");

            entity.HasIndex(e => e.IdeventType, "IX_EventType")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.IdeventType).HasColumnName("IDEventType");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.LocationId).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MonthlyWorkHour>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);
        });

        modelBuilder.Entity<OffType>(entity =>
        {
            entity.HasKey(e => e.IdoffType)
                .HasName("PK_OffTime")
                .HasFillFactor(90);

            entity.ToTable("OffType");

            entity.Property(e => e.IdoffType).HasColumnName("IDOffType");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OffWorkSheet>(entity =>
        {
            entity.HasKey(e => e.IdoffWorkSheet).HasFillFactor(90);

            entity.ToTable("OffWorkSheet");

            entity.Property(e => e.IdoffWorkSheet).HasColumnName("IDOffWorkSheet");
            entity.Property(e => e.IdoffType).HasColumnName("IDOffType");
            entity.Property(e => e.IdworkSheet).HasColumnName("IDWorkSheet");

            entity.HasOne(d => d.IdoffTypeNavigation).WithMany(p => p.OffWorkSheets)
                .HasForeignKey(d => d.IdoffType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OffWorkSheet_OffTime");

            entity.HasOne(d => d.IdworkSheetNavigation).WithMany(p => p.OffWorkSheets)
                .HasForeignKey(d => d.IdworkSheet)
                .HasConstraintName("FK_OffWorkSheet_WorkSheet");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Post");

            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Smkdocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK_DocumentId");

            entity.ToTable("SMKDocument");

            entity.Property(e => e.DocumentId)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PostDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Smkdocument2DepartamentPost>(entity =>
        {
            entity.HasKey(e => e.Smkdocument2DepartamentPostId).HasName("PK__SMKDocum__A297DCE8B0C426C9");

            entity.ToTable("SMKDocument2DepartamentPost");

            entity.Property(e => e.Smkdocument2DepartamentPostId)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("SMKDocument2DepartamentPostId");
            entity.Property(e => e.DocumentId)
                .HasMaxLength(32)
                .IsUnicode(false);

            entity.HasOne(d => d.Departament).WithMany(p => p.Smkdocument2DepartamentPosts)
                .HasForeignKey(d => d.DepartamentId)
                .HasConstraintName("FK__SMKDocume__Depar__02925FBF");

            entity.HasOne(d => d.Document).WithMany(p => p.Smkdocument2DepartamentPosts)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("FK__SMKDocume__Docum__00AA174D");

            entity.HasOne(d => d.Post).WithMany(p => p.Smkdocument2DepartamentPosts)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__SMKDocume__PostI__019E3B86");
        });

        modelBuilder.Entity<Smkdocument2Employee>(entity =>
        {
            entity.HasKey(e => e.Smkdocument2EmployeeId).HasName("PK_SMKDocument2EmployeeId");

            entity.ToTable("SMKDocument2Employee");

            entity.Property(e => e.Smkdocument2EmployeeId)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("SMKDocument2EmployeeId");
            entity.Property(e => e.DocumentId)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.FamiliarizeDate).HasColumnType("datetime");

            entity.HasOne(d => d.Document).WithMany(p => p.Smkdocument2Employees)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("FK_SMKDocument2Employee_SMKDocument");

            entity.HasOne(d => d.Employee).WithMany(p => p.Smkdocument2Employees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SMKDocument2Employee_Employee");
        });

        modelBuilder.Entity<Smkdocument2Post>(entity =>
        {
            entity.HasKey(e => e.Smkdocument2PostId).HasName("PK_SMKDocument2PostId");

            entity.ToTable("SMKDocument2Post");

            entity.Property(e => e.Smkdocument2PostId)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("SMKDocument2PostId");
            entity.Property(e => e.DocumentId)
                .HasMaxLength(32)
                .IsUnicode(false);

            entity.HasOne(d => d.Document).WithMany(p => p.Smkdocument2Posts)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SMKDocument2Post_SMKDocument");

            entity.HasOne(d => d.Post).WithMany(p => p.Smkdocument2Posts)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SMKDocument2Post_Post");
        });

        modelBuilder.Entity<WorkEvent>(entity =>
        {
            entity.HasKey(e => e.IdworkEvent).HasFillFactor(90);

            entity.ToTable("WorkEvent");

            entity.HasIndex(e => e.DateEvent, "IX_WorkEvent").HasFillFactor(90);

            entity.HasIndex(e => e.PrevEvent, "WorkEvent_DateEvent_PrevEvent");

            entity.HasIndex(e => new { e.Idemployee, e.DateEvent }, "WorkEvent_IDEventType_IDOffType_Note");

            entity.Property(e => e.IdworkEvent).HasColumnName("IDWorkEvent");
            entity.Property(e => e.AutomatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateEvent).HasColumnType("datetime");
            entity.Property(e => e.Idemployee).HasColumnName("IDEmployee");
            entity.Property(e => e.IdeventType).HasColumnName("IDEventType");
            entity.Property(e => e.IdoffType).HasColumnName("IDOffType");
            entity.Property(e => e.Note)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.RegEmployee)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdemployeeNavigation).WithMany(p => p.WorkEvents)
                .HasForeignKey(d => d.Idemployee)
                .HasConstraintName("FK_WorkEvent_Employee");

            entity.HasOne(d => d.IdeventTypeNavigation).WithMany(p => p.WorkEvents)
                .HasForeignKey(d => d.IdeventType)
                .HasConstraintName("FK_WorkEvent_EventType");

            entity.HasOne(d => d.IdoffTypeNavigation).WithMany(p => p.WorkEvents)
                .HasForeignKey(d => d.IdoffType)
                .HasConstraintName("FK_WorkEvent_OffType");

            entity.HasOne(d => d.JoinWithGoawayNavigation).WithMany(p => p.InverseJoinWithGoawayNavigation)
                .HasForeignKey(d => d.JoinWithGoaway)
                .HasConstraintName("FK_WorkEvent_WorkEvent1");

            entity.HasOne(d => d.PrevEventNavigation).WithMany(p => p.InversePrevEventNavigation)
                .HasForeignKey(d => d.PrevEvent)
                .HasConstraintName("FK_WorkEvent_WorkEvent");
        });

        modelBuilder.Entity<WorkSheet>(entity =>
        {
            entity.HasKey(e => e.IdworkSheet).HasFillFactor(90);

            entity.ToTable("WorkSheet");

            entity.Property(e => e.IdworkSheet).HasColumnName("IDWorkSheet");
            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Idemployee).HasColumnName("IDEmployee");

            entity.HasOne(d => d.IdemployeeNavigation).WithMany(p => p.WorkSheets)
                .HasForeignKey(d => d.Idemployee)
                .HasConstraintName("FK_WorkSheet_Employee");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
