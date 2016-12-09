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

        public UrlController(IDataAccessLayer dataAccessLayer)
        {
            _dataAccess = dataAccessLayer;
            _hahGenerator = new BijectiveHashGenerator();
        }
        
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateShortUrl(CreateRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Url))
            {
                return BadRequest("URL cannot be empty");
            }

            var inputUrl = request.Url;
            var itemId = _dataAccess.Create(inputUrl.Trim());
            var hash = _hahGenerator.ConvertIdToHash(itemId);
            var tinyUrl = string.Format(_baseAddress, hash);
            return Ok(tinyUrl);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [Route("api/url/GetShortUrl/{shortUrlHash}")]
        public IHttpActionResult GetShortUrl(string shortUrlHash)
        {
            if (string.IsNullOrWhiteSpace(shortUrlHash))
            {
                return BadRequest("URL cannot be empty");
            }

            var actualUrl = GetRedirectUrl(shortUrlHash.Trim());
            return Ok(actualUrl);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]            
        public HttpResponseMessage Get(string urlHash)
        {
            if (string.IsNullOrWhiteSpace(urlHash))
            {
                throw new InvalidOperationException("Bad request");
            }

            var redirectUrl = GetRedirectUrl(urlHash.Trim());         
            var response = Request.CreateResponse(HttpStatusCode.Moved);            
            response.Headers.Location = new Uri(redirectUrl);
            return response;
        }

        private string GetRedirectUrl(string urlHash)
        {           
            var id = _hahGenerator.ConvertHashToId(urlHash);
            return _dataAccess.GetUrl(id) ?? string.Empty;
        }

    }
}
