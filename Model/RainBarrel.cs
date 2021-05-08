namespace Model
{
    public class RainBarrel : Container
    {
        private const double CAPACITY_DEFAULT = 100;
        private const double CAPACITY_SMALL = 80;
        private const double CAPACITY_LARGE = 120;

        private RainBarrel(double capacity, double content) : base(capacity, content) { }

        public static RainBarrel Get()
        {
            return Get(0);
        }

        public static RainBarrel Get(double content)
        {
            return new RainBarrel(CAPACITY_DEFAULT, content);
        }

        public static RainBarrel GetSmall()
        {
            return GetSmall(0);
        }

        public static RainBarrel GetSmall(double content)
        {
            return new RainBarrel(CAPACITY_SMALL, content);
        }

        public static RainBarrel GetLarge()
        {
            return GetLarge(0);
        }

        public static RainBarrel GetLarge(double content)
        {
            return new RainBarrel(CAPACITY_LARGE, content);
        }
    }
}
