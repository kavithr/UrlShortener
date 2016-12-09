using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UrlShortener.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ConvertIdToHashShouldReturnEqualantLetterForInput1()
        {
            var generator = new UrlShortener.Convertors.BijectiveHashGenerator();
            var actual = generator.ConvertIdToHash(10000);
            Assert.AreEqual("b", actual);          
        }
    }
}
