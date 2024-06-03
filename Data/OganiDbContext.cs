using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Ogani.ViewModel;

namespace Ogani.Data
{
    public class OganiDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public OganiDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Page>(x =>
            {
                x.ToTable("Pages");
                x.HasData(new Page()
                {
                    Id = new Guid("3d5a6cad-25cd-4aef-9d16-3f22d6d5d717"),
                    Name = "Home Page",
                    TotalView = 0
                });
            });
            builder.Entity<Blog>(x =>
            {
                x.ToTable("Blogs");
                x.HasKey(x => x.Id);
                x.HasOne(x => x.AppUser).WithMany(x => x.Blogs).HasForeignKey(x => x.UserId);
            });
            builder.Entity<Product>(x =>
            {
                x.ToTable("Products");
                x.HasKey(x => x.Id);
                x.HasOne(x => x.Supplier).WithMany(x => x.Products).HasForeignKey(x => x.SupplierId);
                x.HasData(new Product()
                {
                    CreateAt = DateTime.Now,
                    CurrentPrice = "100",
                    Description = "Feature",
                    SupplierId = new Guid("ab77aefb-5a93-4fa6-abfb-5c904d7ad5b8"),
                    Name = "Feature-1",
                    Id = new Guid("ed040235-219c-48d8-a12d-3ae4d89a2fb9"),
                    ReducePrice = "200",
                    ToTalRemaining = 5,
                    Rate = 0,
                    Image = "/img/featured/feature-1.jpg"
                });
                x.HasData(new Product()
                {
                    CreateAt = DateTime.Now,
                    CurrentPrice = "500",
                    Description = "Feature",
                    SupplierId = new Guid("ab77aefb-5a93-4fa6-abfb-5c904d7ad5b8"),
                    Name = "Feature-2",
                    Id = Guid.NewGuid(),
                    ReducePrice = "200",
                    ToTalRemaining = 5,
                    Image = "/img/featured/feature-2.jpg"
                });
            });
            builder.Entity<ProductImage>(x =>
            {
                x.ToTable("ProductImages");
                x.HasKey(x => x.Id);
                x.HasOne(x => x.Product).WithMany(x => x.ProductImages).HasForeignKey(x => x.ProductId);
                x.HasData(new ProductImage()
                {
                    Id = new Guid("3c9a6cad-25cd-4aef-9d16-3f22d6d5d717"),
                    Name = "Feature-3",
                    ProductId = new Guid("ed040235-219c-48d8-a12d-3ae4d89a2fb9")
                });
            });

            builder.Entity<ProductOrder>(x =>
            {
                x.ToTable("ProductOrders");
                x.HasKey(x => new { x.ProductId, x.OrderId });
                x.HasOne(x => x.Product).WithMany(x => x.ProductOrders).HasForeignKey(x => x.ProductId);
                x.HasOne(x => x.Order).WithMany(x => x.ProductOrders).HasForeignKey(x => x.OrderId);
            });
            builder.Entity<Order>(x =>
            {
                x.ToTable("Orders");
                x.HasKey(x => x.Id);
                x.HasOne(x => x.AppUser).WithMany(x => x.Orders).HasForeignKey(x => x.UserId);
            });
            builder.Entity<Supplier>(x =>
            {
                x.ToTable("Suppliers");
                x.HasKey(x => x.Id);
                x.HasData(new Supplier()
                {
                    Id = new Guid("ab77aefb-5a93-4fa6-abfb-5c904d7ad5b8"),
                    Address = "Ha noi",
                    Name = " Nguoi ha noi"
                });
            });
            builder.Entity<Category>(x =>
            {
                x.ToTable("Categories");
                x.HasKey(x => x.Id);
                x.HasData(new Category() { Id = new Guid("695b5cdb-0992-488e-8963-e76093bb5905"), Name = "Vegetables", Description = "" });
                x.HasData(new Category() { Id = Guid.NewGuid(), Name = "Meat", Description = "" });
                x.HasData(new Category() { Id = Guid.NewGuid(), Name = "Oranges", Description = "" });
                x.HasData(new Category() { Id = Guid.NewGuid(), Name = "Fastfood", Description = "" });
                x.HasData(new Category() { Id = Guid.NewGuid(), Name = "Fresh Bananas", Description = "" });
                x.HasData(new Category() { Id = Guid.NewGuid(), Name = "Drink Fruits", Description = "" });
                x.HasData(new Category() { Id = Guid.NewGuid(), Name = "Sea Food", Description = "" });
            });

            builder.Entity<ProductCategory>(x =>
            {
                x.ToTable("ProductCategories");
                x.HasKey(x => new { x.ProductId, x.CategoryId });
                x.HasOne(x => x.Product).WithMany(x => x.ProductCategories).HasForeignKey(x => x.ProductId);
                x.HasOne(x => x.Category).WithMany(x => x.ProductCategories).HasForeignKey(x => x.CategoryId);
                x.HasData(new ProductCategory()
                {
                    CategoryId = new Guid("695b5cdb-0992-488e-8963-e76093bb5905"),
                    ProductId = new Guid("ed040235-219c-48d8-a12d-3ae4d89a2fb9")
                });
            });

            var hasher = new PasswordHasher<AppUser>();
            builder.Entity<AppUser>().HasData(new AppUser
            {
                Id = new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672"),
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "1"),
                SecurityStamp = string.Empty,
                PhoneNumber = "02002012",
            });
            builder.Entity<AppRole>(x =>
            {
                x.HasData(new AppRole()
                {
                    Id = new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"),
                    Name = "admin",
                    NormalizedName = "Admin",
                });
                x.HasData(new AppRole()
                {
                    Id = Guid.NewGuid(),
                    Name = "employee",
                    NormalizedName = "Employee",
                });
            });
            builder.Entity<AppUserRole>().HasData(new AppUserRole
            {
                RoleId = new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"),
                UserId = new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672")
            });

            builder.Entity<AppUserRole>(x =>
            {
                x.HasKey(x => new { x.UserId, x.RoleId });
                x.HasOne(x => x.AppUser).WithMany(x => x.AppUserRoles).HasForeignKey(x => x.UserId);
                x.HasOne(x => x.AppRole).WithMany(x => x.AppUserRoles).HasForeignKey(x => x.RoleId);
            });
            //IdentityUserLogin
            builder.Entity<IdentityUserLogin<Guid>>().HasKey(x => new { x.UserId, x.ProviderKey });
            //IdentityUserToken
            builder.Entity<IdentityUserToken<Guid>>().HasKey(x => x.UserId);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Page> Pages { get; set; }
    }
}