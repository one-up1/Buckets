using Buckets.Common.Event;
using System;

namespace Buckets.Common.Model
{
    public sealed class Bucket : Container
    {
        private const double CAPACITY_DEFAULT = 12;
        private const double CAPACITY_MIN = 10;

        private Bucket(double capacity, double content) : base(capacity, content) { }

        public void Fill(Bucket bucket, double amount, bool force)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount",
                    "amount must be greater than 0");
            }

            if (force)
            {
                Empty(amount);
                bucket.Fill(amount, true);
            }
            else
            {
                double content = bucket.Content + amount;
                if (content > bucket.Capacity)
                    bucket.OnFull(new BucketOverflowEventArgs(amount, content - bucket.Capacity, this));
                else Fill(bucket, amount, true);
            }
        }

        public static Bucket GetDefault()
        {
            return GetDefault(0);
        }

        public static Bucket GetDefault(double content)
        {
            return new Bucket(CAPACITY_DEFAULT, content);
        }

        public static Bucket Get(double capacity)
        {
            return Get(capacity, 0);
        }

        public static Bucket Get(double capacity, double content)
        {
            if (capacity < CAPACITY_MIN)
            {
                throw new ArgumentOutOfRangeException("capacity",
                    $"A bucket cannot be smaller than {CAPACITY_MIN}");
            }
            return new Bucket(capacity, content);
        }
    }
}
