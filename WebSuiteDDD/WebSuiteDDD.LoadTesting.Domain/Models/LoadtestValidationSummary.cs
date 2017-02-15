namespace WebSuiteDDD.LoadTesting.Domain.Models
{
    public class LoadtestValidationSummary
    {
        public bool OkToAddOrModify { get; set; }
        public string ReasonForValidationFailure { get; set; }
    }
}