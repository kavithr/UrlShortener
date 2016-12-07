using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UrlShortner.DAL;


namespace UrlShortener.Controllers
{
    public class UrlController : ApiController
    {
        

        // GET: api/Url/5
        public string Get(int url)
        {
            return "value";
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult CreateShortUrl(string url)
        {
            IDataAccessLayer data = new DyanamoDB();
            var shortUrl = data.CreateUrl(url, 1);
            return Ok(shortUrl);
        }
        
        // POST: api/Url
        public void Post([FromBody]string value)
        {
            IDataAccessLayer data = new DyanamoDB();
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
