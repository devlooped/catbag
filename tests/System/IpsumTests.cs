#nullable enable
using Xunit;

namespace System
{
    public class IpsumTests
    {
        [Fact]
        public void WhenGettingWords_ThenEndsWithDot()
        {
            var phrase = Ipsum.GetPhrase(10);

            Assert.EndsWith(".", phrase);
        }

        [Fact]
        public void WhenGetting10Words_ThenStartsWithLoremIpsum()
        {
            var phrase = Ipsum.GetPhrase(10);

            Assert.StartsWith("Lorem ipsum dolor sit amet", phrase);
        }

        [Fact]
        public void WhenGetting3Words_ThenStartsWithLoremIpsumDolor()
        {
            var phrase = Ipsum.GetPhrase(3);

            Assert.Equal("Lorem ipsum dolor.", phrase);
        }
    }
}
