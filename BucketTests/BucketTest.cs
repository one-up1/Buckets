using Buckets.Common.Model;
using System;
using Xunit;

namespace BucketTests
{
    public class BucketTest
    {
        [Fact]
        public void CreateContainers()
        {
            Container container;

            // Test whether default containers have the correct capacity and content.
            container = Bucket.GetDefault();
            Assert.Equal(12, container.Capacity);
            Assert.Equal(0, container.Content);

            container = RainBarrel.Get();
            Assert.Equal(100, container.Capacity);
            Assert.Equal(0, container.Content);

            container = RainBarrel.GetSmall();
            Assert.Equal(80, container.Capacity);
            Assert.Equal(0, container.Content);

            container = RainBarrel.GetLarge();
            Assert.Equal(120, container.Capacity);
            Assert.Equal(0, container.Content);

            container = OilBarrel.Get();
            Assert.Equal(159, container.Capacity);
            Assert.Equal(0, container.Content);

            // Test whether or not an exception is thrown when creating valid/invalid conainers.
            Bucket.GetDefault(11);
            Bucket.GetDefault(12);
            Assert.Throws<ArgumentOutOfRangeException>(() => Bucket.GetDefault(13));

            Bucket.Get(11);
            Bucket.Get(10);
            Assert.Throws<ArgumentOutOfRangeException>(() => Bucket.Get(9));

            Bucket.Get(16, 15);
            Bucket.Get(16, 16);
            Assert.Throws<ArgumentOutOfRangeException>(() => Bucket.Get(16, 17));

            RainBarrel.Get(99);
            RainBarrel.Get(100);
            Assert.Throws<ArgumentOutOfRangeException>(() => RainBarrel.Get(101));

            RainBarrel.GetSmall(79);
            RainBarrel.GetSmall(80);
            Assert.Throws<ArgumentOutOfRangeException>(() => RainBarrel.GetSmall(81));

            RainBarrel.GetLarge(119);
            RainBarrel.GetLarge(120);
            Assert.Throws<ArgumentOutOfRangeException>(() => RainBarrel.GetLarge(121));

            OilBarrel.Get(158);
            OilBarrel.Get(159);
            Assert.Throws<ArgumentOutOfRangeException>(() => OilBarrel.Get(160));
        }

        /*[Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Test1(int i)
        {
            Assert.Equal(4, i + 1);
        }*/
    }
}
