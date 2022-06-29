using System;
using Moq;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class AddLazyExtensionTests
    {
        [Fact]
        public void CanResolveLazily()
        {
            var services = new ServiceCollection();
            services.AddLazy()
                .AddSingleton(_ => Mock.Of<IMyService>())
                .AddTransient<Lazily>();

            var container = services.BuildServiceProvider();

            var lazy = container.GetRequiredService<Lazily>();

            Assert.NotNull(lazy.MyService.Value);
        }

        public interface IMyService { }

        record Lazily(Lazy<IMyService> MyService);
    }
}