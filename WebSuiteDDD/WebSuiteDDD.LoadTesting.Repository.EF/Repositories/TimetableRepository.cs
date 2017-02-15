using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using WebSuiteDDD.LoadTesting.Domain;
using WebSuiteDDD.LoadTesting.Domain.Models;
using WebSuiteDDD.LoadTesting.Domain.Repositories;

namespace WebSuiteDDD.LoadTesting.Repository.EF.Repositories
{
    public class TimetableRepository : ITimetableRepository
    {
        public IList<Loadtest> GetLoadtestsForTimePeriod(DateTime searchStartDateUtc, DateTime searchEndDateUtc)
        {
            var context = new LoadTestingContext();
            IList<Loadtest> loadtestsInSearchPeriod = (from loadtest in context.Loadtests
                                                       where (loadtest.Parameters.StartDateUtc <= searchStartDateUtc
                                                              &&
                                                              SqlFunctions.DateAdd("s", loadtest.Parameters.DurationSec, loadtest.Parameters.StartDateUtc) >=
                                                              searchStartDateUtc)
                                                             ||
                                                             (loadtest.Parameters.StartDateUtc <= searchEndDateUtc
                                                              &&
                                                              SqlFunctions.DateAdd("s", loadtest.Parameters.DurationSec, loadtest.Parameters.StartDateUtc) >=
                                                              searchEndDateUtc)
                                                             ||
                                                             (loadtest.Parameters.StartDateUtc <= searchStartDateUtc
                                                              &&
                                                              SqlFunctions.DateAdd("s", loadtest.Parameters.DurationSec, loadtest.Parameters.StartDateUtc) >=
                                                              searchEndDateUtc)
                                                             ||
                                                             (loadtest.Parameters.StartDateUtc >= searchStartDateUtc
                                                              &&
                                                              SqlFunctions.DateAdd("s", loadtest.Parameters.DurationSec, loadtest.Parameters.StartDateUtc) <=
                                                              searchEndDateUtc)
                                                       select loadtest).ToList();
            return loadtestsInSearchPeriod;
        }

        public void AddOrUpdateLoadtests(AddOrUpdateLoadtestsValidationResult addOrUpdateLoadtestsValidationResult)
        {
            var context = new LoadTestingContext();
            if (addOrUpdateLoadtestsValidationResult.ValidationComplete)
            {
                if (addOrUpdateLoadtestsValidationResult.ToBeInserted.Any())
                {
                    foreach (var toBeInserted in addOrUpdateLoadtestsValidationResult.ToBeInserted)
                    {
                        context.Entry(toBeInserted).State = EntityState.Added;
                    }
                }

                if (addOrUpdateLoadtestsValidationResult.ToBeUpdated.Any())
                {
                    foreach (var toBeUpdated in addOrUpdateLoadtestsValidationResult.ToBeUpdated)
                    {
                        context.Entry(toBeUpdated).State = EntityState.Modified;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException(
                    "Validation is not complete. You have to call the AddOrUpdateLoadtests method of the Timetable class first.");
            }

            context.SaveChanges();
        }

        public void DeleteById(Guid guid)
        {
            var context = new LoadTestingContext();
            var loadtest = (from l in context.Loadtests
                            where l.Id == guid
                            select l).FirstOrDefault();

            if (loadtest == null) throw new ArgumentException($"There's no load test by ID {guid}");

            context.Entry(loadtest).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}