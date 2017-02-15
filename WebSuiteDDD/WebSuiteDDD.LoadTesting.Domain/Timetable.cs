using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSuiteDDD.LoadTesting.Domain.Models;
using WebSuiteDDD.SharedKernel;

namespace WebSuiteDDD.LoadTesting.Domain
{
    public class Timetable : IAggregateRoot
    {
        public Timetable(List<Loadtest> loadtests)
        {
            if (loadtests == null)
            {
                loadtests = new List<Loadtest>();
            }

            this.Loadtests = loadtests;
        }

        public List<Loadtest> Loadtests { get; }

        public AddOrUpdateLoadtestsValidationResult AddOrUpdateLoadtests(IEnumerable<Loadtest> loadtestsAddedOrUpdated)
        {
            var toBeInserted = new List<Loadtest>();
            var toBeUpdated = new List<Loadtest>();
            var failed = new List<Loadtest>();
            var resultSummaryBuilder = new StringBuilder();
            var NL = Environment.NewLine;
            foreach (var loadtest in loadtestsAddedOrUpdated)
            {
                var existing = (from l in Loadtests where l.Id == loadtest.Id select l).FirstOrDefault();
                if (existing != null) //update
                {
                    var validationSummary = OkToAddOrModify(loadtest);
                    if (validationSummary.OkToAddOrModify)
                    {
                        existing.Update
                            (loadtest.Parameters, loadtest.AgentId, loadtest.CustomerId,
                                loadtest.EngineerId, loadtest.LoadtestTypeId, loadtest.ProjectId, loadtest.ScenarioId);
                        toBeUpdated.Add(existing);
                        resultSummaryBuilder.Append($"Load test ID {existing.Id} (update) successfully validated.{NL}");
                    }
                    else
                    {
                        failed.Add(loadtest);
                        resultSummaryBuilder.Append(
                            $"Load test ID {existing.Id} (update) validation failed: {validationSummary.ReasonForValidationFailure}{NL}.");
                    }
                }
                else //insertion
                {
                    var validationSummary = OkToAddOrModify(loadtest);
                    if (validationSummary.OkToAddOrModify)
                    {
                        Loadtests.Add(loadtest);
                        toBeInserted.Add(loadtest);
                        resultSummaryBuilder.Append(
                            $"Load test ID {loadtest.Id} (insertion) successfully validated.{NL}");
                    }
                    else
                    {
                        failed.Add(loadtest);
                        resultSummaryBuilder.Append(
                            $"Load test ID {loadtest.Id} (insertion) validation failed: {validationSummary.ReasonForValidationFailure}{NL}.");
                    }
                }
            }

            return new AddOrUpdateLoadtestsValidationResult(toBeInserted, toBeUpdated, failed,
                resultSummaryBuilder.ToString());
        }

        private LoadtestValidationSummary OkToAddOrModify(Loadtest loadtest)
        {
            var validationSummary = new LoadtestValidationSummary
            {
                OkToAddOrModify = true,
                ReasonForValidationFailure = string.Empty
            };

            var loadtestsOnSameAgent = (from l in Loadtests
                                        where l.AgentId == loadtest.AgentId
                                              && DatesOverlap(l, loadtest)
                                        select l)
                .ToList();

            if (loadtestsOnSameAgent.Count >= 2)
            {
                validationSummary.OkToAddOrModify = false;
                validationSummary.ReasonForValidationFailure +=
                    " The selected load test agent is already booked for this period. ";
            }

            if (loadtest.EngineerId.HasValue)
            {
                var loadtestsOnSameEngineer = (from l in Loadtests
                                               where l.EngineerId.Value == loadtest.EngineerId.Value
                                                     && DatesOverlap(l, loadtest)
                                               select l)
                    .ToList();

                if (loadtestsOnSameEngineer.Any())
                {
                    validationSummary.OkToAddOrModify = false;
                    validationSummary.ReasonForValidationFailure +=
                        " The selected load test engineer is already booked for this period. ";
                }
            }

            return validationSummary;
        }

        private bool DatesOverlap(Loadtest loadtestOne, Loadtest loadtestTwo)
        {
            return (loadtestOne.Parameters.StartDateUtc < loadtestTwo.Parameters.GetEndDateUtc()
                    && loadtestTwo.Parameters.StartDateUtc < loadtestOne.Parameters.GetEndDateUtc());
        }
    }
}