#nullable enable
using Xunit;
using Xunit.Abstractions;

namespace System
{
    public class Base62Tests(ITestOutputHelper Output)
    {
        [Fact]
        public void CanRoundtripValue()
        {
            var expected = DateTimeOffset.UtcNow.Ticks;

            var code = Base62.Encode(expected);

            Output.WriteLine(code);

            var actual = Base62.Decode(code);

            Assert.Equal(expected, actual);
        }
    }
}
