using System.Collections.Generic;
using Xunit;

#nullable enable
namespace System
{
    public class StringFormatNamed
    {
        [Fact]
        public void ReplaceWithDictionary()
        {
            var template = "https://foo.com/{path}/{value:#,#.##}/{id:n}";
            var id = Guid.NewGuid();
            var path = "folder";
            var value = 42.424242;
            var values = new Dictionary<string, object?>
            {
                { "path", path},
                { "value", value },
                { "id", id }
            };

            var actual = template.FormatNamed(new()
            {
                { "path", path },
                { "value", value },
                { "id", id }
            });

            Assert.Equal($"https://foo.com/{path}/{value:#,#.##}/{id:n}", actual);
        }

        [Fact]
        public void ReplaceWithDictionarySupportsNulls()
        {
            var template = "https://foo.com/{path}/{value:#,#.##}/{id:n}";
            Guid? id = null;
            string? path = null;
            double? value = null;
            var values = new Dictionary<string, object?>
            {
                { "path", path },
                { "value", value },
                { "id", id }
            };

            var actual = template.FormatNamed(values);
            Assert.Equal($"https://foo.com/{path}/{value:#,#.##}/{id:n}", actual);
        }

        [Fact]
        public void ReplaceWithAnonymousObject()
        {
            var template = "https://foo.com/{path}/{value:#,#.##}/{id:n}";
            var id = Guid.NewGuid();
            var path = "folder";
            var value = 42.424242;
            var values = new
            {
                path = path,
                value = value,
                id = id
            };

            var actual = template.FormatNamed(values);
            Assert.Equal($"https://foo.com/{path}/{value:#,#.##}/{id:n}", actual);
        }

        [Fact]
        public void ReplaceWithAnonymousObjectCaseInsensitive()
        {
            var template = "https://foo.com/{path}/{value:#,#.##}/{id:n}";
            var id = Guid.NewGuid();
            var path = "folder";
            var value = 42.424242;
            var values = new
            {
                Path = path,
                Value = value,
                ID = id
            };

            var actual = template.FormatNamed(values);
            Assert.Equal($"https://foo.com/{path}/{value:#,#.##}/{id:n}", actual);
        }
    }
}
