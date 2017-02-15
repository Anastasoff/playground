using System.Collections.Generic;

namespace WebSuiteDDD.LoadTesting.Domain.Models
{
    public class AddOrUpdateLoadtestsValidationResult
    {
        public AddOrUpdateLoadtestsValidationResult(List<Loadtest> toBeInserted, List<Loadtest> toBeUpdated,
            List<Loadtest> failed, string operationResultSummary)
        {
            this.ToBeInserted = toBeInserted;
            this.ToBeUpdated = toBeUpdated;
            this.Failed = failed;
            this.OperationResultSummary = operationResultSummary;
            this.ValidationComplete = (toBeInserted != null && toBeUpdated != null && failed != null &&
                                       !string.IsNullOrEmpty(operationResultSummary));
        }

        public List<Loadtest> ToBeInserted { get; }
        public List<Loadtest> ToBeUpdated { get; }
        public List<Loadtest> Failed { get; }
        public string OperationResultSummary { get; }
        public bool ValidationComplete { get; }
    }
}