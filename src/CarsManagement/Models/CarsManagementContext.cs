using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarsManagement.Models
{
    public partial class CarsManagementContext : DbContext
    {
        public CarsManagementContext(DbContextOptions<CarsManagementContext>Options):base(Options)
        {

        }

        public CarsManagementContext()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectsV13;Database=CarsManagement;Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categry>(entity =>
            {
                entity.Property(e => e.CatName)
                    .HasColumnName("cat_name")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductDescription)
                    .HasColumnName("product_description")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductImage)
                    .HasColumnName("product_image")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductName)
                    .HasColumnName("product_name")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductPrice)
                    .HasColumnName("product_price")
                    .HasMaxLength(50);

                entity.Property(e => e.SubCatId).HasColumnName("sub_cat_id");
            });

            modelBuilder.Entity<SubCategry>(entity =>
            {
                entity.Property(e => e.CatId)
                    .HasColumnName("cat_Id")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasMaxLength(50);

                entity.Property(e => e.SubCatName)
                    .HasColumnName("sub_cat_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasMaxLength(50);

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("email_address")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .HasMaxLength(50);
            });
        }

        public virtual DbSet<Categry> Categry { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<SubCategry> SubCategry { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}