using ITech.Data.Entites;
using ITech.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITech.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        public DbSet<Seed> Seeds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Seller>()
                .HasOne(s => s.User)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Notification>()
                .HasOne(n => n.Receiver)
                .WithMany(r => r.Notifications)
                .HasForeignKey(n => n.ReceiverId);


            builder.Entity<Notification>()
                .HasOne(n => n.Sender)
                .WithMany( s => s.SentNotifications)
                .HasForeignKey(n => n.SenderId);
                

            base.OnModelCreating(builder);
        }
    }
}
