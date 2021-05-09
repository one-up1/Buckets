using System;

namespace Buckets.Common.Model
{
    public delegate void FullHandler(object sender, ContainerFullEventArgs e);

    public abstract class Container
    {
        public event FullHandler Full;

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

        public override string ToString()
        {
            return $"{capacity} {GetType().Name}";
        }

        public void Fill(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount",
                    "amount must be greater than 0");
            }
            content += amount;

            if (content == capacity)
            {
                OnContainerFull(new ContainerFullEventArgs());
            }
            else if (content > capacity)
            {
                OnContainerFull(new ContainerFullEventArgs(content - capacity));
                content = capacity;
            }
        }

        public void Fill(Bucket bucket)
        {
            Fill(bucket, content);
        }

        public void Fill(Bucket bucket, double amount)
        {
            Empty(amount);
            bucket.Fill(amount);
        }

        public void Empty(double amount)
        {
            if (content == 0)
            {
                throw new InvalidOperationException("Bucket is empty");
            }
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount",
                    "amount must be greater than 0");
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

        protected void OnContainerFull(ContainerFullEventArgs e)
        {
            FullHandler full = Full;
            if (full != null)
            {
                full(this, e);
            }
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
