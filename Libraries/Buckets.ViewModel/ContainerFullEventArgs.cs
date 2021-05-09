using System;

namespace Buckets.ViewModel
{
    public class ContainerFullEventArgs : EventArgs
    {
        public ContainerFullEventArgs() { }

        public ContainerFullEventArgs(double overflow)
        {
            Overflow = overflow;
        }

        public double Overflow { get; set; }
    }
}
