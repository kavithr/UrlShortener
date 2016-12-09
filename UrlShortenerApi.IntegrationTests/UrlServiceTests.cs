using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using UrlShortenerApi.IntegrationTests;

namespace UrlShortenerApi.EndToEndTests
{
    [TestClass]
    public class CreateShortUrlTests
    {
        const string BaseUrl = "http://urlshortener-env.us-west-2.elasticbeanstalk.com/api/Url/";
        const string CreateShortUrlEndPoint = BaseUrl + "CreateShortUrl";
        const string GetLongUrlEndpoint = BaseUrl + "GetLongUrl/{0}";

        [TestInitialize()]
        public void Startup()
        {
            // TODO : Setup resources
        }

        [TestCleanup()]
        public void Cleanup()
        {
            // Cleanup resources
          
        }

        [TestMethod]
        public void CreateShortUrl_GetLongUrl_RetrievesNewlyCreatedLongUrl()
        {
            const string LongTestUrl = "https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api";

            using (var client = new HttpClient())
            {
                var request = new
                {
                    Url = LongTestUrl,
                };

                var response = HttpClientUtil.PostJson(client, request, CreateShortUrlEndPoint);

                Assert.IsTrue(response.IsSuccessStatusCode);
                var tinyUrl = HttpClientUtil.GetResponseResult(response);

                const string TinyUrlApiSuffix = "api/Url";

                Assert.IsNotNull(tinyUrl);
                Assert.IsTrue(tinyUrl.Contains(TinyUrlApiSuffix));

                var urlParts = tinyUrl.Split('/');
                var urlHash = urlParts[urlParts.Length - 1];

                response = client.GetAsync(string.Format(GetLongUrlEndpoint, urlHash)).Result;                
                Assert.IsTrue(response.IsSuccessStatusCode);
                Assert.IsNotNull(response);
                var longUrl = HttpClientUtil.GetResponseResult(response);
                Assert.AreEqual(request.Url, longUrl);
            }
        }


        [TestMethod]
        public void CreateShortUrl_DuplicateLongUrl_ShouldReturnExistingShortUrl()
        {
            const string LongTestUrl = "https://msdn.microsoft.com/en-us/library/system.net.http.httpclientextensions.postasjsonasync.aspx";
            const string ExpectedTinyUrl = BaseUrl + "p";

            using (var client = new HttpClient())
            {
                var request = new
                {
                    Url = LongTestUrl,
                };

                var response = HttpClientUtil.PostJson(client, request, CreateShortUrlEndPoint);

                Assert.IsNotNull(response);
                Assert.IsTrue(response.IsSuccessStatusCode);
                var tinyUrl = HttpClientUtil.GetResponseResult(response);
                Assert.AreEqual(ExpectedTinyUrl, tinyUrl);
            }
        }
    }
}
