using System;

namespace Buckets.Model
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
