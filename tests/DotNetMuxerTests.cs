using System.IO;
using System.Runtime.InteropServices;
using Xunit;

namespace System;

public class DotNetMuxerTests
{
    static string ExpectedMuxerFileName =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "dotnet.exe" : "dotnet";

    [Fact]
    public void MuxerPathIsSet()
    {
        Assert.NotNull(DotNetMuxer.MuxerPath);
    }

    [Fact]
    public void MuxerPathPointsToExistingFile()
    {
        Assert.NotNull(DotNetMuxer.MuxerPath);
        Assert.True(File.Exists(DotNetMuxer.MuxerPath), $"Muxer not found at '{DotNetMuxer.MuxerPath}'");
    }

    [Fact]
    public void MuxerPathIsDotnetExecutable()
    {
        Assert.NotNull(DotNetMuxer.MuxerPath);
        Assert.Equal(ExpectedMuxerFileName, Path.GetFileName(DotNetMuxer.MuxerPath), ignoreCase: true);
    }

    [Fact]
    public void MuxerPathOrDefaultReturnsPathWhenResolved()
    {
        var path = DotNetMuxer.MuxerPathOrDefault();
        Assert.False(string.IsNullOrEmpty(path));

        if (DotNetMuxer.MuxerPath is not null)
            Assert.Equal(DotNetMuxer.MuxerPath, path);
        else
            Assert.Equal("dotnet", path);
    }

    [Fact]
    public void MuxerPathCanBeResolvedFromDotnetHostPathOrRoot()
    {
        // Documents the env-var fallbacks used when the runtime-directory walk fails
        // (Native AOT). In a normal testhost run the walk already succeeds; still verify
        // the same locations the fallbacks would use are valid when present.
        if (Environment.GetEnvironmentVariable("DOTNET_HOST_PATH") is { Length: > 0 } host
            && File.Exists(host))
        {
            Assert.Equal(ExpectedMuxerFileName, Path.GetFileName(host), ignoreCase: true);
            Assert.NotNull(DotNetMuxer.MuxerPath);
            return;
        }

        if (Environment.GetEnvironmentVariable("DOTNET_ROOT") is { Length: > 0 } root)
        {
            var fromRoot = Path.Combine(root, ExpectedMuxerFileName);
            if (File.Exists(fromRoot))
            {
                Assert.NotNull(DotNetMuxer.MuxerPath);
                return;
            }
        }

        // No env fallbacks set — still expect the main-module / runtime-dir probes to work.
        Assert.NotNull(DotNetMuxer.MuxerPath);
        Assert.True(File.Exists(DotNetMuxer.MuxerPath));
    }
}
