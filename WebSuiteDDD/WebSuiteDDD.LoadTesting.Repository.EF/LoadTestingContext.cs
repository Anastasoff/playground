using System.Data.Entity;
using WebSuiteDDD.LoadTesting.Domain;

namespace WebSuiteDDD.LoadTesting.Repository.EF
{
    public class LoadTestingContext : DbContext
    {
        public LoadTestingContext() : base("WebSuiteContext")
        {
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Engineer> Engineers { get; set; }
        public DbSet<Loadtest> Loadtests { get; set; }
        public DbSet<LoadtestType> LoadtestTypes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
    }
}