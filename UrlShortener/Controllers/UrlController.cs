using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UrlShortener.Convertors;
using UrlShortner.DAL;


namespace UrlShortener.Controllers
{
    public class UrlController : ApiController
    {
        private readonly IDataAccessLayer _dataAccess;
        private const string _baseAddress = "http://urlshortener-env.us-west-2.elasticbeanstalk.com/api/Url/{0}";
        IHashGenerator _hahGenerator;

        public UrlController(IDataAccessLayer dataAccessLayer, IHashGenerator hahGenerator)
        {
            _dataAccess = dataAccessLayer;
            _hahGenerator = hahGenerator;
        }
        
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateShortUrl(CreateTinyUrlRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Url))
            {
                return BadRequest("Invalid create request");
            }

            var inputUrl = request.Url;
            var itemId = _dataAccess.Create(inputUrl.Trim());
            var hash = _hahGenerator.ConvertIdToHash(itemId);
            var tinyUrl = string.Format(_baseAddress, hash);
            return Ok(tinyUrl);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/url/GetLongUrl/{shortUrlHash}")]
        public IHttpActionResult GetLongtUrl(string shortUrlHash)
        {
            if (string.IsNullOrWhiteSpace(shortUrlHash))
            {
                return BadRequest("URL cannot be empty");
            }

            var longUrl = GetRedirectUrl(shortUrlHash.Trim());
            return Ok(longUrl);
        }

        /// <summary>
        /// Redirect to original url
        /// </summary>
        /// <param name="urlHash"></param>
        /// <returns></returns>
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]            
        public HttpResponseMessage Get(string urlHash)
        {
            if (string.IsNullOrWhiteSpace(urlHash))
            {
                throw new InvalidOperationException("Bad request");
            }

            var longUrl = GetRedirectUrl(urlHash.Trim());

            if (string.IsNullOrWhiteSpace(longUrl))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
                 
            var response = Request.CreateResponse(HttpStatusCode.Moved);            
            response.Headers.Location = new Uri(longUrl);
            return response;
        }

        private string GetRedirectUrl(string urlHash)
        {           
            var id = _hahGenerator.ConvertHashToId(urlHash);
            return _dataAccess.GetUrl(id) ?? string.Empty;
        }

    }
}
