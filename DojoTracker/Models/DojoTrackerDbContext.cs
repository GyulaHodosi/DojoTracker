using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DojoTracker.Models;

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
            modelBuilder.Entity<Dojo>().HasData(DojoList.Dojos);
            modelBuilder.Entity<User>().Property(user => user.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Solution>().Property(solution => solution.Id).ValueGeneratedOnAdd();
        }

        public DbSet<Dojo> Dojos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Solution> Solutions { get; set; }

    }
}
