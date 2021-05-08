using System;

namespace Buckets.Model
{
    public sealed class Bucket : Container
    {
        private const double CAPACITY_DEFAULT = 12;
        private const double CAPACITY_MIN = 10;

        private Bucket(double capacity, double content) : base(capacity, content) { }

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
