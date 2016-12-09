using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortenerApi.IntegrationTests
{
    public class HttpClientUtil
    {
        public static string GetResponseResult(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
        }

        public static HttpResponseMessage PostJson(HttpClient client, object request, string url)
        {
            return client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(request).ToString(),
                        Encoding.UTF8, "application/json"))
                        .Result;
        }
    }
}
