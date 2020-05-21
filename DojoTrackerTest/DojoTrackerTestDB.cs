using DojoTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace TestDojoTracker
{
    class DojoTrackerTestDB : IDisposable
    {
        internal readonly DojoTrackerDbContext _context;

        public DojoTrackerTestDB()
        {
            var options = new DbContextOptionsBuilder<DojoTrackerDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DojoTrackerDbContext(options);
            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
