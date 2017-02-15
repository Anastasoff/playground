using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using WebSuiteDDD.Repository.EF.DataModel;

namespace WebSuiteDDD.Repository.EF.Tests.WebSuiteDataMigrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WebSuiteContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"WebSuiteDataMigrations";
        }

        protected override void Seed(WebSuiteContext context)
        {
            var agents = new List<Agent>();
            var amazon = new Agent
            {
                Id = Guid.NewGuid(),
                Location = new Location
                {
                    City = "Seattle",
                    Country = "USA",
                    Latitude = 123.345,
                    Longitude = 135.543
                }
            };
            var rackspace = new Agent
            {
                Id = Guid.NewGuid(),
                Location = new Location
                {
                    City = "Frankfurt",
                    Country = "Germany",
                    Latitude = -123.654,
                    Longitude = 121.321
                }
            };
            var azure = new Agent
            {
                Id = Guid.NewGuid(),
                Location = new Location
                {
                    City = "Tokyo",
                    Country = "Japan",
                    Latitude = 23.45,
                    Longitude = 12.343
                }
            };
            agents.Add(amazon);
            agents.Add(rackspace);
            agents.Add(azure);
            context.Agents.AddRange(agents);

            var customers = new List<Customer>();
            var niceCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                Address = "New York",
                MainContact = "Elvis Presley",
                Name = "Nice customer"
            };

            var greatCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                Address = "London",
                MainContact = "Phil Collins",
                Name = "Great customer"
            };

            var okCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                Address = "Berlin",
                MainContact = "Freddie Mercury",
                Name = "OK Customer"
            };

            customers.Add(niceCustomer);
            customers.Add(greatCustomer);
            customers.Add(okCustomer);
            context.Customers.AddRange(customers);

            var engineers = new List<Engineer>();
            var john = new Engineer
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Title = "Load test engineer",
                YearJoinedCompany = 2013
            };

            var mary = new Engineer
            {
                Id = Guid.NewGuid(),
                Name = "Mary",
                Title = "Sr. load test engineer",
                YearJoinedCompany = 2012
            };

            var fred = new Engineer
            {
                Id = Guid.NewGuid(),
                Name = "Fred",
                Title = "Jr. load test engineer",
                YearJoinedCompany = 2014
            };

            engineers.Add(john);
            engineers.Add(mary);
            engineers.Add(fred);
            context.Engineers.AddRange(engineers);

            var testTypes = new List<LoadtestType>();
            var stressTest = new LoadtestType
            {
                Id = Guid.NewGuid(),
                Description = new Description
                {
                    ShortDescription = "Stress test",
                    LongDescription =
                        "To determine or validate an application’s behavior when it is pushed beyond normal or peak load conditions."
                }
            };

            var capacityTest = new LoadtestType
            {
                Id = Guid.NewGuid(),
                Description = new Description
                {
                    ShortDescription = "Capacity test",
                    LongDescription =
                        "To determine how many users and/or transactions a given system will support and still meet performance goals."
                }
            };

            testTypes.Add(stressTest);
            testTypes.Add(capacityTest);
            context.LoadtestTypes.AddRange(testTypes);

            var projects = new List<Project>();
            var firstProject = new Project
            {
                Id = Guid.NewGuid(),
                DateInsertedUtc = DateTime.UtcNow,
                Description = new Description
                {
                    ShortDescription = "First project",
                    LongDescription = "Long description of first project"
                }
            };

            var secondProject = new Project
            {
                Id = Guid.NewGuid(),
                DateInsertedUtc = DateTime.UtcNow.AddDays(-5),
                Description = new Description
                {
                    ShortDescription = "Second project",
                    LongDescription = "Long description of second project"
                }
            };

            var thirdProject = new Project
            {
                Id = Guid.NewGuid(),
                DateInsertedUtc = DateTime.UtcNow.AddDays(-10),
                Description = new Description
                {
                    ShortDescription = "Third project",
                    LongDescription = "Long description of third project"
                }
            };
            projects.Add(firstProject);
            projects.Add(secondProject);
            projects.Add(thirdProject);
            context.Projects.AddRange(projects);

            var scenarios = new List<Scenario>();
            var scenarioOne = new Scenario
            {
                Id = Guid.NewGuid(),
                UriOne = "www.bbc.co.uk",
                UriTwo = "www.cnn.com"
            };

            var scenarioTwo = new Scenario
            {
                Id = Guid.NewGuid(),
                UriOne = "www.amazon.com",
                UriTwo = "www.microsoft.com"
            };

            var scenarioThree = new Scenario
            {
                Id = Guid.NewGuid(),
                UriOne = "www.greatsite.com",
                UriTwo = "www.nosuchsite.com",
                UriThree = "www.neverheardofsite.com"
            };

            scenarios.Add(scenarioOne);
            scenarios.Add(scenarioTwo);
            scenarios.Add(scenarioThree);
            context.Scenarios.AddRange(scenarios);

            var loadtests = new List<Loadtest>();
            var ltOne = new Loadtest
            {
                Id = Guid.NewGuid(),
                AgentId = amazon.Id,
                CustomerId = niceCustomer.Id,
                EngineerId = john.Id,
                LoadtestTypeId = stressTest.Id,
                Parameters = new LoadtestParameters { DurationSec = 60, StartDateUtc = DateTime.UtcNow, UserCount = 10 },
                ProjectId = firstProject.Id,
                ScenarioId = scenarioOne.Id
            };

            var ltTwo = new Loadtest
            {
                Id = Guid.NewGuid(),
                AgentId = azure.Id,
                CustomerId = greatCustomer.Id,
                EngineerId = mary.Id,
                LoadtestTypeId = capacityTest.Id,
                Parameters = new LoadtestParameters
                {
                    DurationSec = 120,
                    StartDateUtc = DateTime.UtcNow.AddMinutes(20),
                    UserCount = 40
                },
                ProjectId = secondProject.Id,
                ScenarioId = scenarioThree.Id
            };

            var ltThree = new Loadtest
            {
                Id = Guid.NewGuid(),
                AgentId = rackspace.Id,
                CustomerId = okCustomer.Id,
                EngineerId = fred.Id,
                LoadtestTypeId = stressTest.Id,
                Parameters = new LoadtestParameters
                {
                    DurationSec = 180,
                    StartDateUtc = DateTime.UtcNow.AddMinutes(30),
                    UserCount = 50
                },
                ProjectId = thirdProject.Id,
                ScenarioId = scenarioTwo.Id
            };

            loadtests.Add(ltOne);
            loadtests.Add(ltTwo);
            loadtests.Add(ltThree);
            context.Loadtests.AddRange(loadtests);

            context.SaveChanges();
        }
    }
}