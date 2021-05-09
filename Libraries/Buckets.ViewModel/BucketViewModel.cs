using Buckets.Common.Model;

namespace Buckets.ViewModel
{
    public class BucketViewModel : ContainerViewModel
    {
        public BucketViewModel(Bucket bucket) : base(bucket) { }

        public void Fill(BucketViewModel bucket)
        {
            Fill(bucket, Content);
        }

        public void Fill(BucketViewModel bucket, double amount)
        {
            Empty(amount);
            bucket.Fill(amount);
        }

        public override string ToString()
        {
            return $"{base.ToString()} bucket";
        }
    }
}
