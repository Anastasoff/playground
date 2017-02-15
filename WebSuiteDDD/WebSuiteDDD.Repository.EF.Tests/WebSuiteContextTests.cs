using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using WebSuiteDDD.Repository.EF.DataModel;
using WebSuiteDDD.Repository.EF.Tests.WebSuiteDataMigrations;
using Xunit;

namespace WebSuiteDDD.Repository.EF.Tests
{
    public class WebSuiteContextTests
    {
        private readonly WebSuiteContext _context;

        public WebSuiteContextTests()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebSuiteContext, Configuration>());
            _context = new WebSuiteContext();
        }

        [Fact]
        public void TestGetAllAgents()
        {
            var dbAgents = _context.Agents.ToList();

            Assert.True(dbAgents.Count >= 3);
        }

        [Fact]
        public void TestToInsertNewEngineer()
        {
            var dbEngineer = new Engineer
            {
                Id = Guid.NewGuid(),
                Name = "Jane",
                Title = "Jr. Load test engineer",
                YearJoinedCompany = 2012
            };

            _context.Engineers.Add(dbEngineer);
            _context.SaveChanges();
        }

        [Fact]
        public void TestToInsertNewEngineerWithNullableTitle()
        {
            var dbEngineer = new Engineer
            {
                Id = Guid.NewGuid(),
                Name = "Jane",
                Title = null,
                YearJoinedCompany = 2012
            };

            _context.Engineers.Add(dbEngineer);
            Assert.Throws<DbUpdateException>(() => { _context.SaveChanges(); });
        }
    }
}