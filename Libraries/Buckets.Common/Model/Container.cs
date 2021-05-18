using Buckets.Common.Event;
using System;

namespace Buckets.Common.Model
{
    public delegate void FullEventHandler(object sender, ContainerFullEventArgs e);

    public abstract class Container
    {
        public event FullEventHandler Full;

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

        public void Fill(double amount, bool force)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount",
                    "amount must be greater than 0");
            }

            double content = Content + amount;
            if (force)
                Content = content > Capacity ? Capacity : content;
            else if (content > Capacity)
                OnFull(new ContainerFullEventArgs(amount, content - Capacity));
            else if ((Content = content) == Capacity)
                OnFull(new ContainerFullEventArgs(amount));
        }

        public void Empty()
        {
            Content = 0;
        }

        public void Empty(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount",
                    "amount must be greater than 0");
            }
            if (Content == 0)
            {
                throw new InvalidOperationException("Container is empty");
            }
            if (Content < amount)
            {
                throw new ArgumentOutOfRangeException("amount",
                    $"There is only {Content} left...");
            }

            Content -= amount;
        }

        protected void OnFull(ContainerFullEventArgs e)
        {
            Full?.Invoke(this, e);
        }
    }
}
