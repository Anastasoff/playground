using System;
using WebSuiteDDD.SharedKernel;

namespace WebSuiteDDD.LoadTesting.Domain
{
    public class Engineer : EntityBase<Guid>
    {
        public Engineer(Guid id, string name) : base(id)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Engineer name");

            Name = name;
        }

        public string Name { get; set; }
    }
}