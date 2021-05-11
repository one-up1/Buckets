using Buckets.Common.Model;
using Buckets.ViewModel;
using Buckets.ViewModel.Event;
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

        [Fact]
        public void FillContainers()
        {
            ContainerViewModel container = new BucketViewModel(Bucket.GetDefault(4));

            // Test whether content is adjused correctly when filling a container.
            container.Fill(2, false);
            Assert.Equal(6, container.Content);

            // Test whether content is adjusted correctly when overflowing a container.
            container.Fill(100, false); // Should not fill when not forcing.
            Assert.Equal(6, container.Content);
            container.Fill(100, true);
            Assert.Equal(container.Capacity, container.Content);

            // Test whether an exception is thrown when passing invalid ammounts.
            Assert.Throws<ArgumentOutOfRangeException>(() => container.Fill(0, false));
            Assert.Throws<ArgumentOutOfRangeException>(() => container.Fill(-2, false));

            // Test whether an event is raised when overflowing a container.
            bool eventRaised = false;
            container.Full += delegate(object sender, ContainerFullEventArgs e)
            {
                eventRaised = true;
            };
            container.Fill(1, false);
            Assert.True(eventRaised);

            // TODO: Test whether no event is raised when not forcing overflow.
            // TODO: Test filling buckets with other buckets.
        }

        [Fact]
        public void EmptyContainers()
        {
            ContainerViewModel container = new OilBarrelViewModel(OilBarrel.Get(100));

            // Test whether content is adjused correctly when emptying a container.
            container.Empty(25);
            Assert.Equal(75, container.Content);
            container.Empty();
            Assert.Equal(0, container.Content);

            // Test whether an exception is thrown when passing invalid ammounts.
            Assert.Throws<ArgumentOutOfRangeException>(() => container.Empty(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => container.Empty(-2));
            
            // Test whether an exception is thrown when trying to empty a container
            // that is already empty or when it contains less than the amount requested.
            Assert.Throws<InvalidOperationException>(() => container.Empty(100));
            container.Fill(100, false);
            Assert.Throws<ArgumentOutOfRangeException>(() => container.Empty(200));
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
