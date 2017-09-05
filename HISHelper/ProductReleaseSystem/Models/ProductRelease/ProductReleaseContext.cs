using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductReleaseSystem.ProductRelease;

namespace ProductReleaseSystem.ProductRelease
{
    public partial class ProductReleaseContext : DbContext
    {
        /// <summary>
        /// 部门表
        /// </summary>
        public virtual DbSet<Departments> Departments { get; set; }
        /// <summary>
        /// 人员表
        /// </summary>
        public virtual DbSet<Developers> Developers { get; set; }
        /// <summary>
        /// 文件表
        /// </summary>
        public virtual DbSet<Files> Files { get; set; }
        /// <summary>
        /// 产品表
        /// </summary>
        public virtual DbSet<Products> Products { get; set; }
        /// <summary>
        /// 在研项目
        /// </summary>
        public virtual DbSet<RelatedPersonnels> RelatedPersonnels { get; set; }
        /// <summary>
        /// 文件表
        /// </summary>
        public virtual DbSet<Users> Users { get; set; }
        /// <summary>
        /// 版本表
        /// </summary>
        public virtual DbSet<Versions> Versions { get; set; }
        /// <summary>
        /// 在研项目表
        /// </summary>
        public virtual DbSet<ResearchProjects> ResearchProjects { get; set; }
        /// <summary>
        /// 在研开发表
        /// </summary>
        public virtual DbSet<Researchers> Researchers { get; set; }
        /// <summary>
        /// 权限表
        /// </summary>
        public virtual DbSet<AuthorityTable> AuthorityTable { get; set; }
        /// <summary>
        /// 需求表
        /// </summary>
        public virtual DbSet<RequestForm> RequestForm { get; set; }
        /// <summary>
        /// 产品需求表
        /// </summary>
        public virtual DbSet<ProductDemandTable> ProductDemandTable { get; set; }
        /// <summary>
        /// 开发分配子需求表
        /// </summary>
        public virtual DbSet<DevelopmentRequirementsTable> DevelopmentRequirementsTable { get; set; }
        /// <summary>
        /// Bug问题表
        /// </summary>
        public virtual DbSet<BugQuestionTable> BugQuestionTable { get; set; }
        /// <summary>
        /// Bug回答表
        /// </summary>
        public virtual DbSet<BuganswerSheet> BuganswerSheet { get; set; }
        /// <summary>
        /// 人员意见表
        /// </summary>
        public virtual DbSet<Opinion> Opinion { get; set; }

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