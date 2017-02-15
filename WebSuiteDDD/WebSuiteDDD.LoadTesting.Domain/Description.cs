using System;
using WebSuiteDDD.SharedKernel;

namespace WebSuiteDDD.LoadTesting.Domain
{
    public class Description : ValueObjectBase<Description>
    {
        public Description(string shortDescription, string longDescription)
        {
            if (string.IsNullOrEmpty(shortDescription))
                throw new ArgumentNullException(nameof(shortDescription), "Short description");

            if (string.IsNullOrEmpty(longDescription))
                throw new ArgumentNullException(nameof(longDescription), "Long description");

            ShortDescription = shortDescription;
            LongDescription = longDescription;
        }

        public string ShortDescription { get; }

        public string LongDescription { get; }

        public Description WithShortDescription(string shortDescription)
        {
            return new Description(shortDescription, this.LongDescription);
        }

        public Description WithLongDescription(string longDescription)
        {
            return new Description(this.ShortDescription, longDescription);
        }

        public override bool Equals(Description other)
        {
            return this.ShortDescription.Equals(other.ShortDescription, StringComparison.InvariantCultureIgnoreCase)
                   && this.LongDescription.Equals(other.LongDescription, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Description)) return false;
            return this.Equals((Description)obj);
        }

        public override int GetHashCode() => this.ShortDescription.GetHashCode() + this.LongDescription.GetHashCode();
    }
}