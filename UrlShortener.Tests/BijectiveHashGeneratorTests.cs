using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UrlShortener.UnitTests
{
    [TestClass]
    public class BijectiveHashGeneratorTests
    {
        [TestMethod]
        public void ConvertIdToHash_ShouldReturn_HashValue()
        {
            var generator = new UrlShortener.Convertors.BijectiveHashGenerator();
            var actual = generator.ConvertIdToHash(1);
            Assert.AreEqual("b", actual);          
        }

        [TestMethod]
        public void ConvertHashToId_ShouldRetun_Id()
        {
            var generator = new UrlShortener.Convertors.BijectiveHashGenerator();
            var actual = generator.ConvertHashToId("b");
            Assert.AreEqual(1, actual);
        }
    }
}
