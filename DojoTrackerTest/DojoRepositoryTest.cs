using DojoTracker.Models;
using DojoTracker.Services.Repositories;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace TestDojoTracker
{
    [TestFixture]
    class DojoRepositoryTest
    {
        private DojoRepository _repostiory;
        private DojoTrackerDbContext _context;

        [SetUp]
        public void Open()
        {
            _context = new DojoTrackerTestDB()._context;
            _repostiory = new DojoRepository(_context);
        }

        [Test]
        public void GetDojoByIdAsync_ValidId_ReturnsDojo()
        {
            //Arrange
            _context.Dojos.Add(new Dojo() { Id = 1, Title = "TestDojo" });
            //Act
            var result = _repostiory.GetDojoByIdAsync(1).Result;
            //Assert
            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.Title, "TestDojo");
        }
        [Test]
        public void GetDojoByIdAsync_InvalidId_ReturnsNull()
        {
            //Arrange
            _context.Dojos.Remove(_context.Dojos.FirstOrDefault(dojo => dojo.Id == 3));
            _context.SaveChanges();
            //Act
            var result = _repostiory.GetDojoByIdAsync(3).Result;
            //Assert
            Assert.IsNull(result);
        }

        [TearDown]
        public void Close()
        {
            _context.Dispose();
        }
    }
}
