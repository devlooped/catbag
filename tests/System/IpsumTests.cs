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

            Assert.StartsWith("Lorem ipsum ", phrase);
        }

        [Fact]
        public void WhenGetting3Words_ThenStartsWithLoremIpsum()
        {
            var phrase = Ipsum.GetPhrase(3);

            Assert.StartsWith("Lorem ipsum", phrase);
        }

        [Fact]
        public void WhenGetting2Words_ThenStartsWithLoremButNoIpsum()
        {
            var phrase = Ipsum.GetPhrase(2);

            Assert.StartsWith("Lorem", phrase);
            Assert.NotEqual("Lorem ipsum.", phrase);
        }

        [Fact]
        public void WhenGetting1Word_ThenDoesNotStartWithLorem()
        {
            var phrase = Ipsum.GetPhrase(1);

            Assert.False(phrase.StartsWith("Lorem"));
        }
    }
}
