using JwtAuthSample.Entity;
using JwtAuthSample.Entity.MoreToMore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthSample.Store
{
    public class JwtDbContext : DbContext
    {
        public JwtDbContext(DbContextOptions<JwtDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categorys { get; set; }

        public DbSet<Product> Products { get; set; }

        #region 多对多
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTags { get; set; } 
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>()
            //    .HasMany(x => x.ProductList)
            //    .WithOne(t => t.CategoryEntity)
            //    .HasPrincipalKey(t => t.Id)
            //    .HasForeignKey(t => t.CategoryId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //HasOne/WithOne 用于引用导航属性和HasMany / WithMany用于集合导航属性。

            //一对多
            modelBuilder.Entity<Product>()
                .HasOne<Category>(x => x.CategoryEntity)
                .WithMany(x => x.ProductList)
                .HasForeignKey(k => k.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);

            //一对一
            modelBuilder.Entity<Product>()
                .HasOne(o => o.Book)
                .WithOne(p => p.product)
                .HasPrincipalKey<Product>(p => p.Id)
                .HasForeignKey<Book>(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostTag>()
            .HasKey(t => new { t.PostId, t.TagId });

        }

        //private void ProductConfValues(EntityTypeBuilder<Product> builder)
        //{

        //}
    }
}
