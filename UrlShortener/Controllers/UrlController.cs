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

        public UrlController()
        {
            _dataAccess = new SQLServerDataAccessLayer();
        }
        

        // GET: api/Url/5
        public string Get(int url)
        {
            return "value";
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult CreateShortUrl(string url = "https://www.asp.net/mvc/overview/getting-started/database-first-development/creating-the-web-application")        {
            
            var itemId = _dataAccess.Create(url);
            var hash = new BijectiveHashGenerator().ConvertIdToHash(itemId);
            var tinyUrl = _tinyUrlPrefix + hash;
            return Ok(tinyUrl);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetUrl(string url)
        {
            url = "http://mytinyurl/b";
            var urlHash = url.Replace(_tinyUrlPrefix, "");
            var id = new BijectiveHashGenerator().ConvertHashToId(urlHash);
            var actualUrl = _dataAccess.GetUrl(id) ?? string.Empty;
            return Ok(actualUrl);
        }

        // POST: api/Url
        public void Post([FromBody]string value)
        {
            IDataAccessLayer data = new SQLServerDataAccessLayer();
        }

        // PUT: api/Url/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Url/5
        public void Delete(int id)
        {
        }
    }
}
