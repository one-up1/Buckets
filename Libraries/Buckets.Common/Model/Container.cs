using System;

namespace Buckets.Common.Model
{
    public abstract class Container
    {
        protected Container(double capacity, double content)
        {
            if (content < 0)
            {
                throw new ArgumentOutOfRangeException("content",
                    "content cannot be less than 0");
            }
            if (content > capacity)
            {
                throw new ArgumentOutOfRangeException("content",
                    $"content cannot be greater than {capacity}");
            }

            Capacity = capacity;
            Content = content;
        }

        public double Capacity { get; set; }

        public double Content { get; set; }
    }
}
