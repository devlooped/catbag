using System.Diagnostics;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace System
{
    public class PreciseTimeTests
    {
        ITestOutputHelper output;

        public PreciseTimeTests(ITestOutputHelper output) => this.output = output;

        [Fact]
        public void WhenGettingNow_ThenThereAreNeverDuplicates()
        {
            var iterations = Stopwatch.Frequency / TimeSpan.TicksPerMillisecond;
            for (var i = 0; i < iterations; i++)
            {
                var first = PreciseTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");
                var second = PreciseTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");

                Assert.NotEqual(first, second);
                // Second take should be greater than first too.
                Assert.Equal(-1, first.CompareTo(second));
            }

            output.WriteLine($"Run for {iterations:N0} iterations");
        }

        [Fact]
        public void WhenGettingNow_ThenValuesAreAlwaysIncrementing()
        {
            var iterations = Stopwatch.Frequency / TimeSpan.TicksPerMillisecond;
            var last = PreciseTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");
            var dupes = 0;
            for (var i = 0; i < iterations; i++)
            {
                var now = PreciseTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");
                if (now == last)
                    dupes++;

                Assert.Equal(-1, last.CompareTo(now));
                last = now;
            }

            output.WriteLine($"Run for {iterations:N0} iterations, tight loop caused {dupes:N0} duplicate values.");
        }
    }
}
