using Buckets.Common.Model;

namespace Buckets.ViewModel
{
    public class OilBarrelViewModel : ContainerViewModel
    {
        public OilBarrelViewModel(OilBarrel oilBarrel) : base(oilBarrel) { }

        public override string ToString()
        {
            return $"{base.ToString()} oil barrel";
        }
    }
}
