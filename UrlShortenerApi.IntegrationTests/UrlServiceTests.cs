using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading;
using System.IO;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace UrlShortenerApi.EndToEndTests
{
    [TestClass]
    public class CreateShortUrlTests
    {
        const string BaseUrl = "http://urlshortener-env.us-west-2.elasticbeanstalk.com/api/Url/";
        const string CreateShortUrl = "http://urlshortener-env.us-west-2.elasticbeanstalk.com/api/Url/CreateShortUrl";
        const string GetShortUrl = "http://urlshortener-env.us-west-2.elasticbeanstalk.com/api/Url/GetShortUrl/{0}";        

        [TestMethod]
        public void CreateAndRetrieveUrlSucceeds()
        {
            const string LongTestUrl = "https://msdn.microsoft.com/en-us/library/system.net.http.httpclientextensions.postasjsonasync.aspx";

            using (var client = new HttpClient())
            {
                var request = new
                {
                    Url = LongTestUrl,
                };

                var response = client.PostAsync(CreateShortUrl, new StringContent(JsonConvert.SerializeObject(request).ToString(),
                        Encoding.UTF8, "application/json"))
                        .Result;

                Assert.IsTrue(response.IsSuccessStatusCode);
                var tinyUrl = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);

                Assert.IsNotNull(tinyUrl);
                Assert.IsTrue(tinyUrl.Contains("api/Url"));

                var urlParts = tinyUrl.Split('/');
                var urlHash = urlParts[urlParts.Length - 1];

                response = client.GetAsync(string.Format(GetShortUrl, urlHash)).Result;                
                Assert.IsTrue(response.IsSuccessStatusCode);
                Assert.IsNotNull(response);
                var longUrl = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                Assert.AreEqual(request.Url, longUrl);
            }
        }
    }
}
