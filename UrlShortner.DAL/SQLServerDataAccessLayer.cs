using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortner.DAL
{
    public class SQLServerDataAccessLayer : IDataAccessLayer
    {
        UrlDetailsEntities db;
        public SQLServerDataAccessLayer()
        {
            db = new UrlDetailsEntities();
        }

        public int Create(string url)
        {
            var urlId = FindUrlByValue(url);
            if(urlId > 0)
            {
                return urlId;
            }
                     
            var item = new UrlDetail
            {
                Url = url
            };

            db.UrlDetails.Add(item);
            var result = db.SaveChanges();

            return result;
        }

        public int FindUrlByValue(string url)
        {
            return db.UrlDetails.FirstOrDefault(item => item.Url.Equals(url, StringComparison.OrdinalIgnoreCase))?.ID ?? 0;
        }

        public string GetUrl(int id)
        {           
            return db.UrlDetails.FirstOrDefault(item => item.ID.Equals(id))?.Url;
        }
    }
}
