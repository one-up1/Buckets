using System;

namespace Buckets.Common.Event
{
    public class ContainerFullEventArgs : EventArgs
    {
        public ContainerFullEventArgs(double amount)
        {
            Amount = amount;
        }

        public ContainerFullEventArgs(double amount, double overflow) : this(amount)
        {
            Overflow = overflow;
        }

        public double Amount { get; }

        public double Overflow { get; }
    }
}
