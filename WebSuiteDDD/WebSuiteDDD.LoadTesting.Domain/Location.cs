using System;
using WebSuiteDDD.SharedKernel;

namespace WebSuiteDDD.LoadTesting.Domain
{
    public class Location : ValueObjectBase<Location>
    {
        public Location(string city, string country)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentNullException(nameof(city), "City");
            if (string.IsNullOrEmpty(country))
                throw new ArgumentNullException(nameof(country), "Country");

            this.City = city;
            this.Country = country;
        }

        public string City { get; }

        public string Country { get; }

        public Location WithCityLocation(string city)
        {
            return new Location(city, this.Country);
        }

        public Location WithCountryLocation(string country)
        {
            return new Location(this.City, country);
        }

        public override bool Equals(Location other)
        {
            return this.City.Equals(other.City, StringComparison.InvariantCultureIgnoreCase)
                   && this.Country.Equals(other.Country, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Location)) return false;
            return this.Equals((Location)obj);
        }

        public override int GetHashCode() => this.City.GetHashCode() + this.Country.GetHashCode();
    }
}