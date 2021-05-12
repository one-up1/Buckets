using Buckets.Common.Model;
using Buckets.ViewModel.Event;
using System;

namespace Buckets.ViewModel
{
    public class BucketViewModel : ContainerViewModel
    {
        public BucketViewModel(Bucket bucket) : base(bucket) { }

        public void Fill(BucketViewModel bucket, double amount, bool force)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount",
                    "amount must be greater than 0");
            }

            if (force)
            {
                Empty(amount);
                bucket.Fill(amount, true);
            }
            else
            {
                double content = bucket.Content + amount;
                if (content > bucket.Capacity)
                    bucket.OnFull(new BucketOverflowEventArgs(amount, content - bucket.Capacity, this));
                else Fill(bucket, amount, true);
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()} bucket";
        }
    }
}
