using Xunit;

namespace System.Threading.Tasks
{
    public class AsyncLazyTests
    {
        [Fact]
        public async Task AwaitValue()
        {
            var lazy = AsyncLazy.Create(() => Task.FromResult(25));

            Assert.Equal(25, await lazy.Value);
        }

        [Fact]
        public async Task AwaitAsyncValue()
        {
            var lazy = AsyncLazy.Create(async () => await Task.FromResult(25));

            Assert.Equal(25, await lazy.Value);
        }

        [Fact]
        public async Task AwaitNewLazyValue()
        {
            var lazy = new AsyncLazy<int>(() => Task.FromResult(25));

            Assert.Equal(25, await lazy.Value);
        }

        [Fact]
        public async Task AwaitNewLazyAsyncValue()
        {
            var lazy = new AsyncLazy<int>(async () => await Task.FromResult(25));

            Assert.Equal(25, await lazy.Value);
        }
    }
}
