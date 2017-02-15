using System;
using WebSuiteDDD.SharedKernel;

namespace WebSuiteDDD.LoadTesting.Domain
{
    public class Agent : EntityBase<Guid>
    {
        public Agent(Guid id, string city, string country) : base(id)
        {
            this.Location = new Location(city, country);
        }

        public Location Location { get; set; }
    }
}