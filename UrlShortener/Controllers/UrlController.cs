using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UrlShortener.Repository;
using UrlShortner.DAL;


namespace UrlShortener.Controllers
{
    public class UrlController : ApiController
    {
        private readonly IDataAccessLayer _dataAccess;
        private const string _tinyUrlPrefix = "http://mytinyurl/";
        IHashGenerator _hahGenerator;

        public UrlController()
        {
            _dataAccess = new SQLServerDataAccessLayer();
            _hahGenerator = new BijectiveHashGenerator();
        }


        //http://urlshortener-env.us-west-2.elasticbeanstalk.com/api/Url/CreateShortUrl/wfhkjewhnbcb21321321adsadsadeee
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult CreateShortUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return BadRequest("URL cannot be empty");
            }

            var itemId = _dataAccess.Create(url.Trim());
            var hash = _hahGenerator.ConvertIdToHash(itemId);
            var tinyUrl = _tinyUrlPrefix + hash;
            return Ok(tinyUrl);
        }

        ////[System.Web.Http.AcceptVerbs("GET")]
        ////[System.Web.Http.HttpGet]
        ////public IHttpActionResult GetUrl(string urlHash)
        ////{
        ////    if (string.IsNullOrWhiteSpace(urlHash))
        ////    {
        ////        return BadRequest("URL cannot be empty");
        ////    }

        ////    var actualUrl = GetRedirectUrl(urlHash.Trim());
        ////    return Ok(actualUrl);
        ////}

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
