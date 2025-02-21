using Xunit;

namespace System;

public class DotNetMuxerTests
{
    [Fact]
    public void MuxerPathIsSet()
    {
        Assert.NotNull(DotNetMuxer.MuxerPath);
    }
}
