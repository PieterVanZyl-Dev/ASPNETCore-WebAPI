using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApi.Models
{
    public partial class DimensionDataAPIContext : DbContext
    {
        public DimensionDataAPIContext()
        {
        }

        public DimensionDataAPIContext(DbContextOptions<DimensionDataAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BusinessTravel> BusinessTravel { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<EducationField> EducationField { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeSurvey> EmployeeSurvey { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<JobRole> JobRole { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; }
        public virtual DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessTravel>(entity =>
            {
                entity.Property(e => e.BusinessTravel1)
                    .IsRequired()
                    .HasColumnName("BusinessTravel")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Department1)
                    .IsRequired()
                    .HasColumnName("Department")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EducationField>(entity =>
            {
                entity.Property(e => e.EducationField1)
                    .IsRequired()
                    .HasColumnName("EducationField")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeNumber);

                entity.Property(e => e.EmployeeNumber).ValueGeneratedOnAdd();



                entity.HasOne(d => d.BusinessTravelNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.BusinessTravel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BusinessTravel");

                entity.HasOne(d => d.DepartmentNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.Department)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department");

                entity.HasOne(d => d.EducationFieldNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.EducationField)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EducationField");

                entity.HasOne(d => d.EmployeeSurveyNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.EmployeeSurvey)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_EmployeeSurvey");

                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.Gender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gender");

                entity.HasOne(d => d.JobRoleNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.JobRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRole");

                entity.HasOne(d => d.MaritalStatusNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.MaritalStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaritalStatus");
            });

            modelBuilder.Entity<EmployeeSurvey>(entity =>
            {

                entity.HasKey(e => e.SurveyId)
                    .HasName("PK__Employee__A5481F9DF3F4ABC8");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.EmployeeNavigation)
                    .WithMany(p => p.EmployeeSurvey1)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeId");
            });


            modelBuilder.Entity<Gender>(entity =>
            {
                entity.Property(e => e.Gender1)
                    .IsRequired()
                    .HasColumnName("Gender")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<JobRole>(entity =>
            {
                entity.Property(e => e.JobRole1)
                    .IsRequired()
                    .HasColumnName("JobRole")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.Property(e => e.MaritalStatus1)
                    .IsRequired()
                    .HasColumnName("MaritalStatus")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsFixedLength();

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsFixedLength();

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeNumber)
                    .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
