using System;

namespace Buckets.Common.Model
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
