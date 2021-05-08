using System;

namespace Model
{
    public delegate void Full(double overflow);

    public abstract class Container
    {
        public event Full Full;

        private double capacity;
        private double content;

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

            this.capacity = capacity;
            this.content = content;
        }

        public void Fill(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount",
                    "amount cannot be less than 0");
            }

            content += amount;
            if (content == capacity)
            {
                Full(0);
            }
            else if (content > capacity)
            {
                Full(content - capacity);
                content = capacity;
            }
        }

        public void Empty(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount",
                    "amount cannot be less than 0");
            }
            if (amount > content)
            {
                throw new ArgumentOutOfRangeException("amount",
                    $"There is only {content} left...");
            }

            content -= amount;
        }

        public void Empty()
        {
            content = 0;
        }

        public double Capacity
        {
            get => capacity;
        }

        public double Content
        {
            get => content;
        }
    }
}
