using Buckets.Common.Model;

namespace Buckets.ViewModel
{
    public class RainBarrelViewModel : ContainerViewModel
    {
        public RainBarrelViewModel(RainBarrel rainBarrel) : base(rainBarrel) { }

        public override string ToString()
        {
            return $"{base.ToString()} bucket";
        }
    }
}
