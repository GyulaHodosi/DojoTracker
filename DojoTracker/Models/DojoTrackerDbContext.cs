using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace DojoTracker.Models
{
    public class DojoTrackerDbContext : IdentityDbContext
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
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                {Name = "Administrator", NormalizedName = "Administrator".ToUpper()});
        }

        public DbSet<Dojo> Dojos { get; set; }
        public DbSet<Solution> Solutions { get; set; }

    }
}
