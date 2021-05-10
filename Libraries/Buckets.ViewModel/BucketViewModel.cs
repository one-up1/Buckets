﻿using Buckets.Common.Model;
using Buckets.ViewModel.Event;

namespace Buckets.ViewModel
{
    public class BucketViewModel : ContainerViewModel
    {
        public BucketViewModel(Bucket bucket) : base(bucket) { }

        public void Fill(BucketViewModel bucket)
        {
            Fill(bucket, Content, false);
        }
        
        public void Fill(BucketViewModel bucket, double amount)
        {
            Fill(bucket, amount, false);
        }

        public void Fill(BucketViewModel bucket, double amount, bool force)
        {
            if (force)
            {
                Empty(amount);
                bucket.Fill(amount, true);
            }
            else
            {
                double content = bucket.Content + amount;
                if (content > bucket.Capacity)
                    bucket.OnFull(new BucketOverflowEventArgs(content - bucket.Capacity, amount, this));
                else
                    Fill(bucket, amount, true);
            }

            /*double content = bucket.Content + amount;
            if (content <= bucket.Capacity || force)
            {
                Empty(amount);
                bucket.Fill(amount, force);
            }
            else
            {
                bucket.OnFull(new BucketOverflowEventArgs(content - bucket.Capacity, amount, this));
            }*/
        }

        public override string ToString()
        {
            return $"{base.ToString()} bucket";
        }
    }
}
