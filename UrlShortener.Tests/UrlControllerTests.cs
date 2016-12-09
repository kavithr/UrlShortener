using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrlShortner.DAL;
using Autofac.Extras.Moq;
using UrlShortener.Controllers;

namespace UrlShortener.Tests
{
    [TestClass]
    public class UrlControllerTests
    {
        [TestMethod]
        public void CreateShortUrl_ShouldReturnData()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock
                mock.Mock<IDataAccessLayer>().Setup(x => x.Create("TestUrl")).Returns(100);
                var controller = mock.Create<UrlController>();

                // Act
                var actual = controller.CreateShortUrl(new CreateRequest { Url = "TestUrl" });
            }  
        }
    }

    
}
