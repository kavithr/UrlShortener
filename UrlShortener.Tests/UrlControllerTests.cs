using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrlShortner.DAL;
using Autofac.Extras.Moq;
using UrlShortener.Controllers;
using UrlShortener.Convertors;
using System.Web.Http.Results;

namespace UrlShortener.UnitTests
{
    [TestClass]
    public class UrlControllerTests
    {
        private const string _baseAddress = "http://urlshortener-env.us-west-2.elasticbeanstalk.com/api/Url/{0}";

        [TestMethod]
        public void CreateShortUrl_ValidInputUrl_ReturnsUrl()
        {   
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                const string ExpectedHash = "abs";
                var expectedUrl = string.Format(_baseAddress, ExpectedHash);
                mock.Mock<IDataAccessLayer>().Setup(x => x.Create("TestUrl")).Returns(100);
                mock.Mock<IHashGenerator>().Setup(x => x.ConvertIdToHash(100)).Returns(ExpectedHash);
                var controller = mock.Create<UrlController>();

                // Act
                var actual = controller.CreateShortUrl(new CreateRequest { Url = "TestUrl" }) as OkNegotiatedContentResult<string>;

                // Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expectedUrl, actual.Content);
            }  
        }

        [TestMethod]
        public void CreateShortUrl_NullInputUrl_ReturnsBadRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var expectedOutput = "URL cannot be empty";
                mock.Mock<IDataAccessLayer>().Setup(x => x.Create(null)).Returns(null);
                mock.Mock<IHashGenerator>().Setup(x => x.ConvertIdToHash(0)).Returns(expectedOutput);
                var controller = mock.Create<UrlController>();

                // Act
                var actual = controller.CreateShortUrl(new CreateRequest { Url = null }) as OkNegotiatedContentResult<string>;

                // Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expectedOutput, actual.Content);
            }
        }
    }

    
}
