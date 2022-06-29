using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class AddAsyncLazyExtensionTests
    {
        [Fact]
        public async void CanResolveLazily()
        {
            var services = new ServiceCollection();
            services.AddAsyncLazy()
                .AddSingleton(async _ =>
                {
                    await Task.Delay(1);
                    return Mock.Of<IMyService>();
                })
                .AddTransient<Lazily>();

            var container = services.BuildServiceProvider();

            var lazy = container.GetRequiredService<Lazily>();

            Assert.Same(await lazy.MyService, await lazy.MyService.Value);
        }

        public interface IMyService { }

        record Lazily(AsyncLazy<IMyService> MyService);
    }
}