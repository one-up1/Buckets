namespace Buckets.ViewModel.Event
{
    public class BucketOverflowEventArgs : ContainerFullEventArgs
    {
        public BucketOverflowEventArgs(double amount, double overflow, BucketViewModel sourceBucket) : base(amount, overflow)
        {
            SourceBucket = sourceBucket;
        }

        public BucketViewModel SourceBucket { get; }
    }
}
