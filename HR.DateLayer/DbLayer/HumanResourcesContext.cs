using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HR.DateLayer.DbLayer
{
    public partial class HumanResourcesContext : DbContext
    {
        public HumanResourcesContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmpPromotion> EmpPromotion { get; set; }
        public virtual DbSet<JobTitle> JobTitle { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(@"Server=localhost;Database=HumanResources;Trusted_Connection=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.DateBirthday).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Inn)
                    .IsRequired()
                    .HasColumnName("INN")
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<EmpPromotion>(entity =>
            {
                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.Salary)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmpPromotion)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EmpPromot__Emplo__145C0A3F");

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.EmpPromotion)
                    .HasForeignKey(d => d.JobTitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EmpPromot__JobTi__15502E78");
            });

            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.Property(e => e.NameJobTitle)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.PhotoPath)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Photo)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Photo__EmployeeI__398D8EEE");
            });
        }
    }
}
