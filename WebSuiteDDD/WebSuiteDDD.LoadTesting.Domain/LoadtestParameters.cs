using System;
using WebSuiteDDD.SharedKernel;

namespace WebSuiteDDD.LoadTesting.Domain
{
    public class LoadtestParameters : ValueObjectBase<LoadtestParameters>
    {
        public LoadtestParameters(DateTime startDateUtc, int userCount, int durationSec)
        {
            if (userCount < 1) throw new ArgumentException("User count cannot be less than 1");
            if (durationSec < 30) throw new ArgumentException("Test duration cannot be less than 30 seconds.");
            if (durationSec > 3600)
                throw new ArgumentException("Test duration cannot be more than 3600 seconds, i.e. 1 hour.");
            if (startDateUtc < DateTime.UtcNow) startDateUtc = DateTime.UtcNow;

            this.StartDateUtc = startDateUtc;
            this.UserCount = userCount;
            this.DurationSec = durationSec;
        }

        public DateTime StartDateUtc { get; }
        public int UserCount { get; }
        public int DurationSec { get; }

        public DateTime GetEndDateUtc()
        {
            return this.StartDateUtc.AddSeconds(this.DurationSec);
        }

        public LoadtestParameters WithStartDateUtc(DateTime newStartDate)
        {
            return new LoadtestParameters(newStartDate, this.UserCount, this.DurationSec);
        }

        public LoadtestParameters WithUserCount(int userCount)
        {
            return new LoadtestParameters(this.StartDateUtc, userCount, this.DurationSec);
        }

        public LoadtestParameters WithDuration(int durationSec)
        {
            return new LoadtestParameters(this.StartDateUtc, this.UserCount, durationSec);
        }

        public override bool Equals(LoadtestParameters other)
        {
            return other.UserCount == this.UserCount
                   && other.StartDateUtc == this.StartDateUtc
                   && other.DurationSec == this.DurationSec;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LoadtestParameters)) return false;
            return this.Equals((LoadtestParameters)obj);
        }

        public override int GetHashCode() => this.DurationSec.GetHashCode() + this.StartDateUtc.GetHashCode() + this.UserCount.GetHashCode();
    }
}