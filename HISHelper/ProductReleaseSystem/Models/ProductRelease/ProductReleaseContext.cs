using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductReleaseSystem.ProductRelease;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class ProductReleaseContext : DbContext
    {
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Developers> Developers { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<RelatedPersonnels> RelatedPersonnels { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Versions> Versions { get; set; }
        public virtual DbSet<ResearchProjects> ResearchProjects { get; set; }
        public virtual DbSet<Researchers> Researchers { get; set; } 

        public ProductReleaseContext(DbContextOptions<ProductReleaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Developers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(e => e.Qq)
                    .IsRequired()
                    .HasColumnName("QQ")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Developers)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Developers_Departments");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.UploadTime).HasColumnType("datetime");

                entity.Property(e => e.VersionsId).HasColumnName("VersionsID");

                entity.HasOne(d => d.UploadPeopleNavigation)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.UploadPeople)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Files_Developers");

                entity.HasOne(d => d.Versions)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.VersionsId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Files_Versions");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<RelatedPersonnels>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Personneltype)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.VersionId).HasColumnName("VersionID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.RelatedPersonnels)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RelatedPersonnels_Developers");

                entity.HasOne(d => d.Version)
                    .WithMany(p => p.RelatedPersonnels)
                    .HasForeignKey(d => d.VersionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RelatedPersonnels_Versions");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Character)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Versions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasColumnName("publisher")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.ReleaseTime).HasColumnType("datetime");

                entity.Property(e => e.VersionNumber)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Versions)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Versions_Products");
            });
        }
    }
}