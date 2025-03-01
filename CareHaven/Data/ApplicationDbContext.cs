using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using CareHaven.Models;
using Microsoft.EntityFrameworkCore;
namespace CareHaven.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Feedback> Feedbacks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Donation>()
                 .HasOne(d => d.User)
                .WithMany(u => u.Donations)
                .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Orphanage)
                .WithMany(o => o.Donations)
                .HasForeignKey(d => d.OrphanageId);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<User> Users { get; set; }

        public DbSet<Orphanage> Orphanages { get; set; }

        public DbSet<Donation> Donations { get; set; }
    }
}
