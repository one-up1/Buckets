namespace Model
{
    public class OilBarrel : Container
    {
        private const int CAPACITY = 159;

        private OilBarrel(int capacity, int content) : base(capacity, content) { }

        public static OilBarrel Get()
        {
            return Get(0);
        }

        public static OilBarrel Get(int content)
        {
            return new OilBarrel(CAPACITY, content);
        }
    }
}
