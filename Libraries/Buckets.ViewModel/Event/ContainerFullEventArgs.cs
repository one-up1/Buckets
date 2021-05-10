using System;

namespace Buckets.ViewModel.Event
{
    public class ContainerFullEventArgs : EventArgs
    {
        public ContainerFullEventArgs() { }

        public ContainerFullEventArgs(double overflow, double amount)
        {
            Overflow = overflow;
            Amount = amount;
        }

        public double Overflow { get; }

        public double Amount { get; }
    }
}
