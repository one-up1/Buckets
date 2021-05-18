using Buckets.Common.Model;

namespace Buckets.Common.Event
{
    public class BucketOverflowEventArgs : ContainerFullEventArgs
    {
        public BucketOverflowEventArgs(double amount, double overflow, Bucket sourceBucket) : base(amount, overflow)
        {
            SourceBucket = sourceBucket;
        }

        public Bucket SourceBucket { get; }
    }
}
