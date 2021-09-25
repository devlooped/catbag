using System.Diagnostics;
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
            for (var i = 0; i < Stopwatch.Frequency; i++)
            {
                var first = PreciseTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");
                var second = PreciseTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");

                Assert.NotEqual(first, second);
            }

            output.WriteLine($"Run for {Stopwatch.Frequency} iterations");
        }
    }
}
