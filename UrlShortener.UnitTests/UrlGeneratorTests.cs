using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Repository;
using Xunit;

namespace UrlShortener.UnitTests
{
    public class UrlGeneratorTests
    {
        [Fact]
        public void EncodeShouldConvertIdToString()
        {
            var urlGenerator = new UrlGenerator();
            var actual = urlGenerator.Encode(1);
            Assert.Equal("aaa", actual);
        }
    }
}
