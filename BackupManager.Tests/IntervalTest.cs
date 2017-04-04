using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BackupManager.Tests
{
    public class IntervalTest
    {
        [Fact]
        public void Test_OverridingOnStart()
        {
            Interval interval1 = new Interval()
            {
                Start = new DateTime(2017, 4, 1),
                End = new DateTime(2017, 4, 10)
            };

            Interval interval2 = new Interval()
            {
                Start = new DateTime(2017, 3, 20),
                End = new DateTime(2017, 4, 5)
            };

            Assert.True(interval1.IsOverriding(interval2));
            Assert.True(interval2.IsOverriding(interval1));
        }

        [Fact]
        public void Test_CompleteOverriding()
        {
            Interval interval1 = new Interval()
            {
                Start = new DateTime(2017, 4, 1),
                End = new DateTime(2017, 4, 10)
            };

            Interval interval2 = new Interval()
            {
                Start = new DateTime(2017, 3, 20),
                End = new DateTime(2017, 4, 15)
            };

            Assert.True(interval1.IsOverriding(interval2));
            Assert.True(interval2.IsOverriding(interval1));
        }

        [Fact]
        public void Test_EqualsOverriding()
        {
            Interval interval1 = new Interval()
            {
                Start = new DateTime(2017, 4, 1),
                End = new DateTime(2017, 4, 10)
            };

            Assert.True(interval1.IsOverriding(interval1));
        }

        [Fact]
        public void Test_NotOverriding()
        {
            Interval interval1 = new Interval()
            {
                Start = new DateTime(2017, 4, 1),
                End = new DateTime(2017, 4, 10)
            };

            Interval interval2 = new Interval()
            {
                Start = new DateTime(2017, 4, 10),
                End = new DateTime(2017, 4, 20)
            };

            Assert.False(interval1.IsOverriding(interval2));
            Assert.False(interval2.IsOverriding(interval1));
        }
    }
}
