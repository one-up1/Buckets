using System;

namespace Model
{
    public abstract class Container
    {
        private int capacity;
        private double content;

        protected Container(int capacity, int content)
        {
            if (content < 0)
            {
                throw new ArgumentOutOfRangeException("content",
                    "content cannot be less than 0");
            }
            if (content > capacity)
            {
                throw new ArgumentOutOfRangeException("content",
                    $"content cannot be greater than {capacity}");
            }

            this.capacity = capacity;
            this.content = content;
        }

        public void Fill(int amount)
        {
            content += amount;
        }

        public void Empty(int amount)
        {
            amount -= amount;
        }

        public void Empty()
        {
            amount = 0;
        }
    }
}
