using Buckets.Common.Model;
using Buckets.ViewModel;
using Buckets.ViewModel.Event;
using System;
using Xunit;

namespace BucketTests
{
    public class BucketTest
    {
        private bool eventRaised;
        private double expectedAmount;
        private double expectedOverflow;
        private BucketViewModel expectedSourceBucket;

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
            BucketViewModel bucket1 = new BucketViewModel(Bucket.GetDefault(6));
            BucketViewModel bucket2 = new BucketViewModel(Bucket.GetDefault(0));

            // Test whether an exception is thrown when passing invalid ammounts.
            Assert.Throws<ArgumentOutOfRangeException>(() => bucket1.Fill(0, false));
            Assert.Throws<ArgumentOutOfRangeException>(() => bucket1.Fill(-2, false));
            Assert.Throws<ArgumentOutOfRangeException>(() => bucket1.Fill(bucket2, 0, false));
            Assert.Throws<ArgumentOutOfRangeException>(() => bucket1.Fill(bucket2, -2, false));

            // Test whether content is adjusted correctly when filling a container.
            bucket1.Fill(2, false);
            Assert.Equal(8, bucket1.Content);

            // Test whether content is adjusted correctly when overflowing a container.
            bucket1.Fill(100, false); // Should not fill when not forcing.
            Assert.Equal(8, bucket1.Content);
            bucket1.Fill(100, true);
            Assert.Equal(bucket1.Capacity, bucket1.Content);

            // Test filling buckets with other buckets.
            bucket1.Fill(bucket2, 4, false);
            Assert.Equal(8, bucket1.Content);
            Assert.Equal(4, bucket2.Content);
            bucket1.Fill(bucket2, bucket1.Content, false);
            Assert.Equal(0, bucket1.Content);
            Assert.Equal(12, bucket2.Content);
            bucket2.Empty();

            // Test events.
            bucket1.Full += Container_Full;
            bucket2.Full += Container_Full;

            // No event should be raised when not filling container to capacity or when forcing.
            TestEvent(0, 0);
            bucket1.Fill(8, false);
            bucket1.Fill(4, true);
            bucket1.Fill(12, true);
            bucket1.Fill(bucket2, 6, false);
            bucket1.Fill(6, true);
            bucket1.Fill(bucket2, 12, true);
            Assert.False(eventRaised);

            // Test whether an event is raised when filling a container to capacity.
            TestEvent(4, 0);
            bucket1.Fill(8, false);
            bucket1.Fill(4, false);
            Assert.True(eventRaised);

            // Test whether an event is raised when overflowing a container.
            bucket1.Empty(4);
            TestEvent(6, 2);
            bucket1.Fill(6, false);
            Assert.True(eventRaised);

            // Test whether an event is raised when overflowing a bucket with another bucket.
            expectedSourceBucket = bucket1;
            TestEvent(12, 12);
            bucket1.Fill(bucket2, 12, false);
            Assert.True(eventRaised);
        }

        private void TestEvent(double expectedAmount, double expectedOverflow)
        {
            this.eventRaised = false;
            this.expectedAmount = expectedAmount;
            this.expectedOverflow = expectedOverflow;
        }

        private void Container_Full(object sender, ContainerFullEventArgs e)
        {
            eventRaised = true;
            Assert.Equal(expectedAmount, e.Amount);
            Assert.Equal(expectedOverflow, e.Overflow);

            if (expectedSourceBucket != null)
            {
                Assert.IsType<BucketOverflowEventArgs>(e);
                Assert.Same(expectedSourceBucket, ((BucketOverflowEventArgs)e).SourceBucket);
            }
        }

        [Fact]
        public void EmptyContainers()
        {
            ContainerViewModel container = new OilBarrelViewModel(OilBarrel.Get(100));

            // Test whether an exception is thrown when passing invalid ammounts.
            Assert.Throws<ArgumentOutOfRangeException>(() => container.Empty(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => container.Empty(-2));

            // Test whether content is adjused correctly when emptying a container.
            container.Empty(25);
            Assert.Equal(75, container.Content);
            container.Empty();
            Assert.Equal(0, container.Content);
            
            // Test whether an exception is thrown when trying to empty a container
            // that is already empty or when it contains less than the amount requested.
            Assert.Throws<InvalidOperationException>(() => container.Empty(100));
            container.Fill(100, false);
            Assert.Throws<ArgumentOutOfRangeException>(() => container.Empty(200));
            container.Empty(); // This method should not throw an exception when container is empty.
            container.Empty();
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
