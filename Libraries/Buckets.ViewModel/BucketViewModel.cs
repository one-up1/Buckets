using Buckets.Common.Model;

namespace Buckets.ViewModel
{
    public class BucketViewModel : ContainerViewModel
    {
        private readonly Bucket bucket;

        public BucketViewModel(Bucket bucket) : base(bucket)
        {
            this.bucket = bucket;
        }

        public void Fill(BucketViewModel bucket, double amount, bool force)
        {
            this.bucket.Fill(bucket.bucket, amount, force);
            OnPropertyChanged();
            bucket.OnPropertyChanged();
        }

        public Bucket Bucket
        {
            get => bucket;
        }

        public override string ToString()
        {
            return $"{base.ToString()} bucket";
        }
    }
}
