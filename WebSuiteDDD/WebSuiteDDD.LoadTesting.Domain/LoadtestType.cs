using System;
using WebSuiteDDD.SharedKernel;

namespace WebSuiteDDD.LoadTesting.Domain
{
    public class LoadtestType : EntityBase<Guid>
    {
        public LoadtestType(Guid guid, string shortDescription, string longDescription)
            : base(guid)
        {
            this.Description = new Description(shortDescription, longDescription);
        }

        public Description Description { get; }
    }
}