using Buckets.Common.Model;
using Buckets.ViewModel.Event;

namespace Buckets.ViewModel
{
    public class BucketViewModel : ContainerViewModel
    {
        public BucketViewModel(Bucket bucket) : base(bucket) { }

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
        }

        public override string ToString()
        {
            return $"{base.ToString()} bucket";
        }
    }
}
