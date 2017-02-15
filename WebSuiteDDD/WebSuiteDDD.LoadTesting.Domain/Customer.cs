using System;
using WebSuiteDDD.SharedKernel;

namespace WebSuiteDDD.LoadTesting.Domain
{
    public class Customer : EntityBase<Guid>
    {
        public Customer(Guid id, string name) : base(id)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Customer name");

            this.Name = name;
        }

        public string Name { get; private set; }

        public void ModifyName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Customer name");

            this.Name = name;
        }
    }
}