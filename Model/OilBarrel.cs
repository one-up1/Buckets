namespace Buckets.Model
{
    public sealed class OilBarrel : Container
    {
        private const double CAPACITY = 159;

        private OilBarrel(double capacity, double content) : base(capacity, content) { }

        public static OilBarrel Get()
        {
            return Get(0);
        }

        public static OilBarrel Get(double content)
        {
            return new OilBarrel(CAPACITY, content);
        }
    }
}