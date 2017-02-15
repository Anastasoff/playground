using System;
using WebSuiteDDD.SharedKernel;

namespace WebSuiteDDD.LoadTesting.Domain
{
    public class Project : EntityBase<Guid>
    {
        public Project(Guid id, string shortDescription, string longDescription) : base(id)
        {
            this.Description = new Description(shortDescription, longDescription);
        }

        public Description Description { get; }
    }
}