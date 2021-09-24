using System.Collections.Generic;
using Xunit;

namespace System
{
    public class QuaranTimeTests
    {
        [Fact]
        public static void QuaranTimeEpoch()
        {
            VerifyDateTime(QuaranTime.Epoch, 2020, 3, 20, 0, 0, 0, 0, TimeSpan.FromHours(-3));
        }

        [Theory]
        [MemberData(nameof(QuaranTime_TestData))]
        public static void ToQuaranTimeMilliseconds(TestTime test)
        {
            long expectedMilliseconds = test.QuaranTimeMilliseconds;
            long actualMilliseconds = test.DateTimeOffset.ToQuaranTimeMilliseconds();
            Assert.Equal(expectedMilliseconds, actualMilliseconds);
        }

        [Theory]
        [MemberData(nameof(QuaranTime_TestData))]
        public static void ToQuaranTimeMilliseconds_RoundTrip(TestTime test)
        {
            long quaranTimeMilliseconds = test.DateTimeOffset.ToQuaranTimeMilliseconds();
            FromQuaranTimeMilliseconds(TestTime.FromMilliseconds(test.DateTimeOffset, quaranTimeMilliseconds));
        }

        [Theory]
        [MemberData(nameof(QuaranTime_TestData))]
        public static void ToQuaranTimeSeconds(TestTime test)
        {
            long expectedSeconds = test.QuaranTimeSeconds;
            long actualSeconds = test.DateTimeOffset.ToQuaranTimeSeconds();
            Assert.Equal(expectedSeconds, actualSeconds);
        }

        [Theory]
        [MemberData(nameof(QuaranTime_TestData))]
        public static void ToQuaranTimeSeconds_RoundTrip(TestTime test)
        {
            long quaranTimeSeconds = test.DateTimeOffset.ToQuaranTimeSeconds();
            FromQuaranTimeSeconds(TestTime.FromSeconds(test.DateTimeOffset, quaranTimeSeconds));
        }

        [Theory]
        [MemberData(nameof(QuaranTime_TestData))]
        public static void FromQuaranTimeMilliseconds(TestTime test)
        {
            // Only assert that expected == actual up to millisecond precision for conversion from milliseconds
            long expectedTicks = (test.DateTimeOffset.UtcTicks / TimeSpan.TicksPerMillisecond) * TimeSpan.TicksPerMillisecond;
            long actualTicks = QuaranTime.FromQuaranTimeMilliseconds(test.QuaranTimeMilliseconds).UtcTicks;
            Assert.Equal(expectedTicks, actualTicks);
        }

        [Fact]
        public static void FromQuaranTimeMilliseconds_Invalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>("milliseconds", () => QuaranTime.FromQuaranTimeMilliseconds(-63720270000001)); // Milliseconds < DateTimeOffset.MinValue
            Assert.Throws<ArgumentOutOfRangeException>("milliseconds", () => QuaranTime.FromQuaranTimeMilliseconds(251817627600000)); // Milliseconds > DateTimeOffset.MaxValue

            Assert.Throws<ArgumentOutOfRangeException>("milliseconds", () => QuaranTime.FromQuaranTimeMilliseconds(long.MinValue)); // Milliseconds < DateTimeOffset.MinValue
            Assert.Throws<ArgumentOutOfRangeException>("milliseconds", () => QuaranTime.FromQuaranTimeMilliseconds(long.MaxValue)); // Milliseconds > DateTimeOffset.MaxValue
        }

        [Theory]
        [MemberData(nameof(QuaranTime_TestData))]
        public static void FromQuaranTimeSeconds(TestTime test)
        {
            // Only assert that expected == actual up to second precision for conversion from seconds
            long expectedTicks = (test.DateTimeOffset.UtcTicks / TimeSpan.TicksPerSecond) * TimeSpan.TicksPerSecond;
            long actualTicks = QuaranTime.FromQuaranTimeSeconds(test.QuaranTimeSeconds).UtcTicks;
            Assert.Equal(expectedTicks, actualTicks);
        }

        [Fact]
        public static void FromQuaranTimeSeconds_Invalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>("seconds", () => QuaranTime.FromQuaranTimeSeconds(-63720270001));// Seconds < DateTimeOffset.MinValue
            Assert.Throws<ArgumentOutOfRangeException>("seconds", () => QuaranTime.FromQuaranTimeSeconds(251817627600)); // Seconds > DateTimeOffset.MaxValue

            Assert.Throws<ArgumentOutOfRangeException>("seconds", () => QuaranTime.FromQuaranTimeSeconds(long.MinValue)); // Seconds < DateTimeOffset.MinValue
            Assert.Throws<ArgumentOutOfRangeException>("seconds", () => QuaranTime.FromQuaranTimeSeconds(long.MaxValue)); // Seconds < DateTimeOffset.MinValue
        }

        [Theory]
        [MemberData(nameof(QuaranTime_TestData))]
        public static void FromQuaranTimeMilliseconds_RoundTrip(TestTime test)
        {
            DateTimeOffset dateTime = QuaranTime.FromQuaranTimeMilliseconds(test.QuaranTimeMilliseconds);
            ToQuaranTimeMilliseconds(TestTime.FromMilliseconds(dateTime, test.QuaranTimeMilliseconds));
        }

        [Theory]
        [MemberData(nameof(QuaranTime_TestData))]
        public static void FromQuaranTimeSeconds_RoundTrip(TestTime test)
        {
            DateTimeOffset dateTime = QuaranTime.FromQuaranTimeSeconds(test.QuaranTimeSeconds);
            ToQuaranTimeSeconds(TestTime.FromSeconds(dateTime, test.QuaranTimeSeconds));
        }

        public static IEnumerable<object[]> QuaranTime_TestData()
        {
            yield return new object[] { TestTime.FromMilliseconds(DateTimeOffset.MinValue, -63720270000000) };
            yield return new object[] { TestTime.FromMilliseconds(DateTimeOffset.MaxValue, 251817627599999) };
            yield return new object[] { TestTime.FromMilliseconds(new DateTimeOffset(2020, 3, 20, 0, 0, 0, TimeSpan.FromHours(-3)), 0) };
            yield return new object[] { TestTime.FromMilliseconds(new DateTimeOffset(2014, 6, 13, 17, 21, 50, TimeSpan.Zero), -181993090000) };
            yield return new object[] { TestTime.FromMilliseconds(new DateTimeOffset(2830, 12, 15, 1, 23, 45, TimeSpan.Zero), 25584416625000) };
            yield return new object[] { TestTime.FromMilliseconds(new DateTimeOffset(2830, 12, 15, 1, 23, 45, 399, TimeSpan.Zero), 25584416625399) };
            yield return new object[] { TestTime.FromMilliseconds(new DateTimeOffset(9999, 12, 30, 23, 24, 25, TimeSpan.Zero), 251817539065000) };
            yield return new object[] { TestTime.FromMilliseconds(new DateTimeOffset(1907, 7, 7, 7, 7, 7, TimeSpan.Zero), -3556641173000) };
            yield return new object[] { TestTime.FromMilliseconds(new DateTimeOffset(1907, 7, 7, 7, 7, 7, 1, TimeSpan.Zero), -3556641172999) };
            yield return new object[] { TestTime.FromMilliseconds(new DateTimeOffset(1907, 7, 7, 7, 7, 7, 777, TimeSpan.Zero), -3556641172223) };
            yield return new object[] { TestTime.FromMilliseconds(new DateTimeOffset(601636288270011234, TimeSpan.Zero), -3556641172999) };
        }

        static void VerifyDateTime(DateTimeOffset dateTime, int year, int month, int day, int hour, int minute, int second, int millisecond, TimeSpan offset)
        {
            Assert.Equal(year, dateTime.Year);
            Assert.Equal(month, dateTime.Month);
            Assert.Equal(day, dateTime.Day);
            Assert.Equal(hour, dateTime.Hour);
            Assert.Equal(minute, dateTime.Minute);
            Assert.Equal(second, dateTime.Second);
            Assert.Equal(millisecond, dateTime.Millisecond);

            Assert.Equal(dateTime.Offset, offset);
        }

        public class TestTime
        {
            TestTime(DateTimeOffset dateTimeOffset, long quaranTimeMilliseconds, long quaranTimeSeconds)
            {
                DateTimeOffset = dateTimeOffset;
                QuaranTimeMilliseconds = quaranTimeMilliseconds;
                QuaranTimeSeconds = quaranTimeSeconds;
            }

            public static TestTime FromMilliseconds(DateTimeOffset dateTimeOffset, long quaranTimeMilliseconds)
            {
                long quaranTimeSeconds = quaranTimeMilliseconds / 1000;

                // Always round QuaranTimeSeconds down toward 1/1/0001 00:00:00
                // (this happens automatically for quaranTimeMilliseconds > 0)
                bool hasSubSecondPrecision = quaranTimeMilliseconds % 1000 != 0;
                if (quaranTimeMilliseconds < 0 && hasSubSecondPrecision)
                {
                    --quaranTimeSeconds;
                }

                return new TestTime(dateTimeOffset, quaranTimeMilliseconds, quaranTimeSeconds);
            }

            public static TestTime FromSeconds(DateTimeOffset dateTimeOffset, long quaranTimeSeconds)
            {
                return new TestTime(dateTimeOffset, quaranTimeSeconds * 1000, quaranTimeSeconds);
            }

            public DateTimeOffset DateTimeOffset { get; private set; }
            public long QuaranTimeMilliseconds { get; private set; }
            public long QuaranTimeSeconds { get; private set; }
        }
    }
}
