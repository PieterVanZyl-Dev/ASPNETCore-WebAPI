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
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<EducationField> EducationField { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<JobRole> JobRole { get; set; }
        public virtual DbSet<MartialStatus> MartialStatus { get; set; }
        public virtual DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessTravel>(entity =>
            {
                entity.Property(e => e.BusinessTravelId).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentId)
                    .HasColumnName("departmentId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.Property(e => e.EducationId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EducationField>(entity =>
            {
                entity.Property(e => e.EducationFieldId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.HasOne(d => d.BusinessTravelNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.BusinessTravel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Busine__71D1E811");

                entity.HasOne(d => d.DepartmentNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.Department)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Depart__72C60C4A");

                entity.HasOne(d => d.EducationFieldNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.EducationField)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Educat__73BA3083");

                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.Gender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Gender__74AE54BC");

                entity.HasOne(d => d.JobRoleNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.JobRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__JobRol__75A278F5");

                entity.HasOne(d => d.MaritalStatusNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.MaritalStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Marita__76969D2E");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.Property(e => e.GenderId)
                    .HasColumnName("genderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<JobRole>(entity =>
            {
                entity.Property(e => e.JobRoleId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MartialStatus>(entity =>
            {
                entity.HasKey(e => e.MaritalStatusId)
                    .HasName("PK__MartialS__C8B1BA720645C318");

                entity.Property(e => e.MaritalStatusId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });


            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
