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
            try
            {
                var itemId = _dataAccess.Create(url);
                var hash = _hahGenerator.ConvertIdToHash(itemId);
                var tinyUrl = _tinyUrlPrefix + hash;
                return Ok(tinyUrl);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetUrl(string url)
        {
            var urlHash = url.Replace(_tinyUrlPrefix, "");
            var id = _hahGenerator.ConvertHashToId(urlHash);
            var actualUrl = _dataAccess.GetUrl(id) ?? string.Empty;
            return Ok(actualUrl);
        }
    }
}
