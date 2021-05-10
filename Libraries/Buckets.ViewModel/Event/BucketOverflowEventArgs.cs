namespace Buckets.ViewModel.Event
{
    public class BucketOverflowEventArgs : ContainerFullEventArgs
    {
        public BucketOverflowEventArgs(double overflow, double amount, BucketViewModel sourceBucket) : base(overflow, amount)
        {
            SourceBucket = sourceBucket;
        }

        public BucketViewModel SourceBucket { get; }
    }
}
