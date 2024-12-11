using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using demo0312.Models;

namespace demo0312.Context;

public partial class User11037Context : DbContext
{
    public User11037Context()
    {
    }

    public User11037Context(DbContextOptions<User11037Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee1> Employee1s { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Purpose> Purposes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.7.159;Database=user11037;Username=user11037;Password=24731");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.IdDep).HasName("department_pk");

            entity.ToTable("department", "schema");

            entity.Property(e => e.IdDep)
                .ValueGeneratedNever()
                .HasColumnName("id_dep");
            entity.Property(e => e.Dep)
                .HasColumnType("character varying")
                .HasColumnName("dep");
        });

        modelBuilder.Entity<Employee1>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("Employee1_pkey");

            entity.ToTable("Employee1", "schema");

            entity.Property(e => e.Userid)
                .ValueGeneratedNever()
                .HasColumnName("userid");
            entity.Property(e => e.EmployeeCode).HasColumnName("employee_code");
            entity.Property(e => e.IdDep).HasColumnName("id_dep");
            entity.Property(e => e.Otdel)
                .HasColumnType("character varying")
                .HasColumnName("otdel");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
            entity.Property(e => e.Userpatronymic)
                .HasMaxLength(100)
                .HasColumnName("userpatronymic");
            entity.Property(e => e.Usersurname)
                .HasMaxLength(100)
                .HasColumnName("usersurname");

            entity.HasOne(d => d.IdDepNavigation).WithMany(p => p.Employee1s)
                .HasForeignKey(d => d.IdDep)
                .HasConstraintName("employee1_department_fk");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("order_pk");

            entity.ToTable("order", "schema");

            entity.Property(e => e.IdOrder)
                .ValueGeneratedNever()
                .HasColumnName("id_order");
            entity.Property(e => e.DateEnd).HasColumnName("date_end");
            entity.Property(e => e.DateNach).HasColumnName("date_nach");
            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.IdPurpose).HasColumnName("id_purpose");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdVisitor).HasColumnName("id_visitor");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("order_employee1_fk");

            entity.HasOne(d => d.IdPurposeNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdPurpose)
                .HasConstraintName("order_purpose_fk");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("order_status_fk");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("order_user_fk");

            entity.HasOne(d => d.IdVisitorNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdVisitor)
                .HasConstraintName("order_visitor_fk");
        });

        modelBuilder.Entity<Purpose>(entity =>
        {
            entity.HasKey(e => e.IdPurpose).HasName("purpose_pk");

            entity.ToTable("purpose", "schema");

            entity.Property(e => e.IdPurpose)
                .ValueGeneratedNever()
                .HasColumnName("id_purpose");
            entity.Property(e => e.Purpose1)
                .HasColumnType("character varying")
                .HasColumnName("purpose");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("role_pkey");

            entity.ToTable("role", "schema");

            entity.Property(e => e.IdRole)
                .ValueGeneratedNever()
                .HasColumnName("id_role");
            entity.Property(e => e.Role1)
                .HasMaxLength(100)
                .HasColumnName("role");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("status_pk");

            entity.ToTable("status", "schema");

            entity.Property(e => e.IdStatus)
                .ValueGeneratedNever()
                .HasColumnName("id_status");
            entity.Property(e => e.Status1)
                .HasColumnType("character varying")
                .HasColumnName("status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("user_pk");

            entity.ToTable("user", "schema");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("id_user");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Mail)
                .HasColumnType("character varying")
                .HasColumnName("mail");
            entity.Property(e => e.Passwd)
                .HasColumnType("character varying")
                .HasColumnName("passwd");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("user_role_fk");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.IdVisitor).HasName("SoloUser_pkey");

            entity.ToTable("Visitor", "schema");

            entity.Property(e => e.IdVisitor)
                .HasDefaultValueSql("nextval('schema.solouser_seq'::regclass)")
                .HasColumnName("id_visitor");
            entity.Property(e => e.Dateofbirth)
                .HasMaxLength(100)
                .HasColumnName("dateofbirth");
            entity.Property(e => e.FileOtherVisitors)
                .HasColumnType("character varying")
                .HasColumnName("file_other_visitors");
            entity.Property(e => e.NomPassp)
                .HasColumnType("character varying")
                .HasColumnName("nom_passp");
            entity.Property(e => e.Organization)
                .HasColumnType("character varying")
                .HasColumnName("organization");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.Primechanie)
                .HasColumnType("character varying")
                .HasColumnName("primechanie");
            entity.Property(e => e.SeriyaPassp)
                .HasColumnType("character varying")
                .HasColumnName("seriya_passp");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
            entity.Property(e => e.Userpatronymic)
                .HasMaxLength(100)
                .HasColumnName("userpatronymic");
            entity.Property(e => e.Usersurname)
                .HasMaxLength(100)
                .HasColumnName("usersurname");
        });
        modelBuilder.HasSequence("Employee1_seq", "schema");
        modelBuilder.HasSequence("Employeee_seq", "schema");
        modelBuilder.HasSequence("role_seq", "schema");
        modelBuilder.HasSequence("solouser_seq", "schema");
        modelBuilder.HasSequence("teamuser_seq", "schema");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
