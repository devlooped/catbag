using System.IO;
using System.Xml.Linq;
using Xunit;

namespace System.Xml
{
    public class BaseUriXmlReaderTest
    {
        [Fact]
        public void SetsBaseUriOnDocument()
        {
            var xml = @"<Project Sdk='Microsoft.NET.Sdk' />";
            var uri = "https://github.com/devlooped/blob/main/catbag.csproj";

            using var inner = XmlReader.Create(new StringReader(xml));

            var doc = XDocument.Load(new BaseUriXmlReader(uri, inner), LoadOptions.SetBaseUri);

            Assert.Equal(uri, doc.BaseUri);
        }
    }
}