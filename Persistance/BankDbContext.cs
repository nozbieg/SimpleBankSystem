using System;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class BankDbContext : DbContext
    {
        public DbSet<BankUser> BankUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=BankDatabase.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BankUser>().HasKey(e => e.Number);
            base.OnModelCreating(modelBuilder);
        }
    }
}
