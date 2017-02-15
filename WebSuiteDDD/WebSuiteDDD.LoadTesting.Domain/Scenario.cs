using System;
using System.Collections.Generic;
using System.Linq;
using WebSuiteDDD.SharedKernel;

namespace WebSuiteDDD.LoadTesting.Domain
{
    public class Scenario : EntityBase<Guid>
    {
        public Scenario(Guid id, IEnumerable<Uri> loadTestSteps) : base(id)
        {
            if (loadTestSteps == null || !loadTestSteps.Any())
            {
                throw new ArgumentException("LoadTest scenario must have at least one valid URI.");
            }

            var uriOne = loadTestSteps.ElementAt(0);
            if (uriOne == null)
                throw new ArgumentException("LoadTest scenario must have at least one valid URI.");

            this.UriOne = uriOne.AbsoluteUri;

            if (loadTestSteps.Count() == 2 && loadTestSteps.ElementAt(1) != null)
            {
                var uriTwo = loadTestSteps.ElementAt(1);
                this.UriTwo = uriTwo.AbsoluteUri;
            }

            if (loadTestSteps.Count() >= 3 && loadTestSteps.ElementAt(1) != null && loadTestSteps.ElementAt(2) != null)
            {
                var uriTwo = loadTestSteps.ElementAt(1);
                this.UriTwo = uriTwo.AbsoluteUri;

                var uriThree = loadTestSteps.ElementAt(2);
                this.UriThree = uriThree.AbsoluteUri;
            }
        }

        public string UriOne { get; }

        public string UriTwo { get; }

        public string UriThree { get; }
    }
}