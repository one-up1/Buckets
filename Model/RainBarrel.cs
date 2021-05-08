namespace Model
{
    public class RainBarrel : Container
    {
        private const int CAPACITY_DEFAULT = 100;
        private const int CAPACITY_SMALL = 80;
        private const int CAPACITY_LARGE = 120;

        private RainBarrel(int capacity, int content) : base(capacity, content) { }

        public static RainBarrel Get()
        {
            return Get(0);
        }

        public static RainBarrel Get(int content)
        {
            return new RainBarrel(CAPACITY_DEFAULT, content);
        }

        public static RainBarrel GetSmall()
        {
            return GetSmall(0);
        }

        public static RainBarrel GetSmall(int content)
        {
            return new RainBarrel(CAPACITY_SMALL, content);
        }

        public static RainBarrel GetLarge()
        {
            return GetLarge(0);
        }

        public static RainBarrel GetLarge(int content)
        {
            return new RainBarrel(CAPACITY_LARGE, content);
        }
    }
}
