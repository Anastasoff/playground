using System;
using System.Collections.Generic;
using WebSuiteDDD.LoadTesting.Domain.Models;

namespace WebSuiteDDD.LoadTesting.Domain.Repositories
{
    public interface ITimetableRepository
    {
        IList<Loadtest> GetLoadtestsForTimePeriod(DateTime searchStartDateUtc, DateTime searchEndDateUtc);
        void AddOrUpdateLoadtests(AddOrUpdateLoadtestsValidationResult addOrUpdateLoadtestsValidationResult);
        void DeleteById(Guid guid);
    }
}