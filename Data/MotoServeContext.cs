using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public partial class MotoServeContext : DbContext
{
    public MotoServeContext()
    {
    }

    public MotoServeContext(DbContextOptions<MotoServeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminAccount> AdminAccounts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAccount> CustomerAccounts { get; set; }

    public virtual DbSet<CustomerMotorcycle> CustomerMotorcycles { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<MaintenanceHistory> MaintenanceHistories { get; set; }

    public virtual DbSet<MaintenanceHistoryType> MaintenanceHistoryTypes { get; set; }

    public virtual DbSet<MaintenanceSchedule> MaintenanceSchedules { get; set; }

    public virtual DbSet<MaintenanceType> MaintenanceTypes { get; set; }

    public virtual DbSet<Mechanic> Mechanics { get; set; }

    public virtual DbSet<MechanicAccount> MechanicAccounts { get; set; }

    public virtual DbSet<PaymentTable> PaymentTables { get; set; }

    public virtual DbSet<Receptionist> Receptionists { get; set; }


    public virtual DbSet<ReceptionistAccount> ReceptionistAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=MotoServe;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ADMIN
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId);
            entity.ToTable("Admin");

            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Firstname).HasMaxLength(100).IsUnicode(false).HasColumnName("firstname");
            entity.Property(e => e.Lastname).HasMaxLength(100).IsUnicode(false).HasColumnName("lastname");
            entity.Property(e => e.Username).HasMaxLength(100).IsUnicode(false).HasColumnName("username");

            entity.HasOne(e => e.AdminAccount)
                .WithOne(a => a.Admin);
        });

        modelBuilder.Entity<AdminAccount>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("AdminAccount");

            entity.HasIndex(e => e.AdminId).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Email).HasMaxLength(255).IsUnicode(false).HasColumnName("email");
            entity.Property(e => e.Password).HasMaxLength(255).IsUnicode(false).HasColumnName("password");
        });

        // CUSTOMER + ACCOUNT
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);
            entity.ToTable("Customers");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Firstname).HasMaxLength(100).IsUnicode(false).HasColumnName("firstname");
            entity.Property(e => e.Lastname).HasMaxLength(100).IsUnicode(false).HasColumnName("lastname");
            entity.Property(e => e.PhoneNumber).HasMaxLength(50).IsUnicode(false).HasColumnName("phone_number");
            entity.Property(e => e.Username).HasMaxLength(100).IsUnicode(false).HasColumnName("username");
            entity.Property(e => e.Motorcycle).HasMaxLength(50).IsUnicode(false).HasColumnName("motorcycle");
            entity.Property(e => e.PlateNumber).HasMaxLength(100).IsUnicode(false).HasColumnName("plate_number");

            entity.HasOne(e => e.CustomerAccount)
                .WithOne(a => a.Customer);
        });

        modelBuilder.Entity<CustomerAccount>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("CustomerAccount");

            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.CustomerId).IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Email).HasMaxLength(255).IsUnicode(false).HasColumnName("email");
            entity.Property(e => e.Password).HasMaxLength(255).IsUnicode(false).HasColumnName("password");
        });

        modelBuilder.Entity<CustomerMotorcycle>(entity =>
{
    entity.HasKey(e => e.CustomerMotorcycleId);
    entity.ToTable("CustomerMotorcycle");

    entity.Property(e => e.CustomerMotorcycleId)
        .HasColumnName("customer_motorcycle_id");

    entity.Property(e => e.CustomerId)
        .HasColumnName("customer_id");

    entity.Property(e => e.Motorcycle)
        .HasMaxLength(200)        
        .IsUnicode(false)         
        .HasColumnName("motorcycle");

    entity.Property(e => e.PlateNumber)
        .HasMaxLength(50)         
        .IsUnicode(false)         
        .HasColumnName("plate_number");
});

modelBuilder.Entity<MaintenanceSchedule>(entity =>
{
    entity.HasKey(e => e.ScheduleId);
    entity.ToTable("MaintenanceSchedule");

    entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
    entity.Property(e => e.Date).HasColumnName("date");
    entity.Property(e => e.Time).HasColumnName("time");
  
    entity.Property(e => e.MechanicId).HasColumnName("mechanic_id");
});

        // INVENTORY
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Inventory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Material).HasMaxLength(100).IsUnicode(false).HasColumnName("material");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)").HasColumnName("price");
            entity.Property(e => e.TotalProfit).HasColumnType("decimal(10, 2)").HasColumnName("total_profit");
        });

        // MAINTENANCE HISTORY
modelBuilder.Entity<MaintenanceHistory>(entity =>
{
    entity.HasKey(e => e.HistoryId);
    entity.ToTable("MaintenanceHistory");

    entity.Property(e => e.HistoryId).HasColumnName("history_id");
    entity.Property(e => e.CustomerId).HasColumnName("customer_id");
    entity.Property(e => e.MechanicId).HasColumnName("mechanic_id");
    entity.Property(e => e.MaintenanceTypeId).HasColumnName("maintenance_type_id");
    entity.Property(e => e.CustomerMotorcycleId).HasColumnName("customer_motorcycle_id");
    entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
    entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)").HasColumnName("amount");

 
});


        // MAINTENANCE TYPE
        modelBuilder.Entity<MaintenanceType>(entity =>
        {
            entity.HasKey(e => e.MaintenanceId);
            entity.ToTable("MaintenanceType");

            entity.Property(e => e.MaintenanceId).HasColumnName("maintenance_id");
            entity.Property(e => e.MaintenanceName).HasMaxLength(150).IsUnicode(false).HasColumnName("maintenance_name");
            entity.Property(e => e.Description).HasMaxLength(255).IsUnicode(false).HasColumnName("description");
            entity.Property(e => e.BasePrice).HasColumnType("decimal(10, 2)").HasColumnName("base_price");
            entity.Property(e => e.MechanicId).HasColumnName("mechanic_id");

            // entity.HasOne(d => d.Mechanic)
            //     .WithMany(p => p.MaintenanceTypes)
            //     .HasForeignKey(d => d.MechanicId)
            //     .OnDelete(DeleteBehavior.SetNull)
            //     .HasConstraintName("FK_MaintenanceType_Mechanic");
        });


        // MECHANIC + ACCOUNT
        modelBuilder.Entity<Mechanic>(entity =>
        {
            entity.HasKey(e => e.MechanicId);
            entity.ToTable("Mechanic");

            entity.Property(e => e.MechanicId).HasColumnName("mechanic_id");
            entity.Property(e => e.Firstname).HasMaxLength(100).IsUnicode(false).HasColumnName("firstname");
            entity.Property(e => e.Lastname).HasMaxLength(100).IsUnicode(false).HasColumnName("lastname");
            entity.Property(e => e.PhoneNumber).HasMaxLength(50).IsUnicode(false).HasColumnName("phone_number");
            entity.Property(e => e.MotorExpertise).HasMaxLength(100).IsUnicode(false).HasColumnName("motor_expertise");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.HasOne<MaintenanceSchedule>()  // if you need relationship
            .WithMany()
            .HasForeignKey(e => e.ScheduleId)
            .OnDelete(DeleteBehavior.Restrict);


            entity.HasOne(e => e.MechanicAccount)
                .WithOne(a => a.Mechanic);
                
        });
        
// // 🔹 MaintenanceHistory has NO ScheduleId anymore
// modelBuilder.Entity<MaintenanceHistory>(entity =>
// {
//     entity.HasKey(e => e.HistoryId);
//     entity.ToTable("MaintenanceHistory");

//     entity.Property(e => e.HistoryId).HasColumnName("history_id");
//     entity.Property(e => e.CustomerId).HasColumnName("customer_id");
//     entity.Property(e => e.MechanicId).HasColumnName("mechanic_id");
//     entity.Property(e => e.MaintenanceTypeId).HasColumnName("maintenance_type_id");
//     entity.Property(e => e.CustomerMotorcycleId).HasColumnName("customer_motorcycle_id");
//     entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)").HasColumnName("amount");

//     // VALID relationships 👇
//     entity.HasOne(e => e.Customer)
//         .WithMany(c => c.MaintenanceHistories)
//         .HasForeignKey(e => e.CustomerId);

//     entity.HasOne(e => e.Mechanic)
//         .WithMany(m => m.MaintenanceHistories)
//         .HasForeignKey(e => e.MechanicId);

//     entity.HasOne(e => e.MaintenanceType)
//         .WithMany(t => t.MaintenanceHistories)
//         .HasForeignKey(e => e.MaintenanceTypeId);

    
// });

        modelBuilder.Entity<MechanicAccount>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("MechanicAccount");

            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.MechanicId).IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MechanicId).HasColumnName("mechanic_id");
            entity.Property(e => e.Email).HasMaxLength(255).IsUnicode(false).HasColumnName("email");
            entity.Property(e => e.Password).HasMaxLength(255).IsUnicode(false).HasColumnName("password");
        });

        // PAYMENT
        modelBuilder.Entity<PaymentTable>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("PaymentTable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Invoice).HasMaxLength(100).IsUnicode(false).HasColumnName("invoice");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)").HasColumnName("amount");
            entity.Property(e => e.PaymentStatus).HasMaxLength(50).IsUnicode(false).HasDefaultValue("Unpaid").HasColumnName("payment_status");
            entity.Property(e => e.MaintenanceId).HasColumnName("maintenance_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Due).HasColumnName("due");

            
        });

        // RECEPTIONIST + ACCOUNT
        modelBuilder.Entity<Receptionist>(entity =>
        {
            entity.HasKey(e => e.ReceptionistId);
            entity.ToTable("Receptionist");

            entity.Property(e => e.ReceptionistId).HasColumnName("receptionist_id");
            entity.Property(e => e.Firstname).HasMaxLength(100).IsUnicode(false).HasColumnName("firstname");
            entity.Property(e => e.Lastname).HasMaxLength(100).IsUnicode(false).HasColumnName("lastname");
            entity.Property(e => e.Username).HasMaxLength(100).IsUnicode(false).HasColumnName("username");

            entity.HasOne(e => e.ReceptionistAccount)
                .WithOne(a => a.Receptionist)
                .HasForeignKey<ReceptionistAccount>(a => a.ReceptionistId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Receptionist_ReceptionistAccount");
        });

        modelBuilder.Entity<ReceptionistAccount>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("ReceptionistAccount");

            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.ReceptionistId).IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ReceptionistId).HasColumnName("receptionist_id");
            entity.Property(e => e.Email).HasMaxLength(255).IsUnicode(false).HasColumnName("email");
            entity.Property(e => e.Password).HasMaxLength(255).IsUnicode(false).HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
