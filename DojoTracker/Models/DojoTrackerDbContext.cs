using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DojoTracker.Models
{
    public class DojoTrackerDbContext : DbContext
    {
        public DojoTrackerDbContext(DbContextOptions<DojoTrackerDbContext> options)
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Dojo>().Property(dojo => dojo.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(user => user.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Solution>().HasNoKey();
        }

        public DbSet<Dojo> Dojos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Solution> Solutions { get; set; }

    }
}
