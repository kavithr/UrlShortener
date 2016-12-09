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
            using (var autoMock = AutoMock.GetLoose())
            {
                // Arrange
                const string ExpectedHash = "abs";
                const string longUrl = "TestUrl";
                var expectedUrl = string.Format(_baseAddress, ExpectedHash);
                autoMock.Mock<IDataAccessLayer>().Setup(x => x.Create(longUrl)).Returns(100);
                autoMock.Mock<IHashGenerator>().Setup(x => x.ConvertIdToHash(100)).Returns(ExpectedHash);
                var controller = autoMock.Create<UrlController>();

                // Act
                var actual = controller.CreateShortUrl(new CreateRequest { Url = longUrl }) as OkNegotiatedContentResult<string>;

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
                var expectedError = "Invalid create request";               
                var controller = mock.Create<UrlController>();

                // Act
                var actual = controller.CreateShortUrl(new CreateRequest { Url = null }) as BadRequestErrorMessageResult;

                // Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expectedError, actual.Message);
            }
        }

        [TestMethod]
        public void CreateShortUrl_NullRequest_ReturnsBadRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var expectedError = "Invalid create request";
                var controller = mock.Create<UrlController>();

                // Act
                var actual = controller.CreateShortUrl(null) as BadRequestErrorMessageResult;

                // Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expectedError, actual.Message);
            }
        }
    }

    
}
