using System;

namespace Model
{
    public class Bucket : Container
    {
        private const int CAPACITY_DEFAULT = 12;
        private const int CAPACITY_MIN = 10;

        private Bucket(int capacity, int content) : base(capacity, content) { }

        public static Bucket GetDefault()
        {
            return GetDefault(0);
        }

        public static Bucket GetDefault(int content)
        {
            return new Bucket(CAPACITY_DEFAULT, content);
        }

        public static Bucket Get(int capacity)
        {
            return Get(capacity, 0);
        }

        public static Bucket Get(int capacity, int content)
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
