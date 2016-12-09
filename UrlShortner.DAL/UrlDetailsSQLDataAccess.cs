using System;
using System.Linq;

namespace UrlShortner.DAL
{
    public class UrlDetailsSQLDataAccess : IDataAccessLayer
    {
        UrlDetailsEntities db;
        public UrlDetailsSQLDataAccess()
        {
            db = new UrlDetailsEntities();
        }

        public int Create(string longUrl)
        {
            var urlId = FindUrlByValue(longUrl);
            if (urlId > -1)
            {
                return urlId;
            }

            var newUrlItem = new UrlDetail
            {
                Url = longUrl
            };

            db.UrlDetails.Add(newUrlItem);
            var result = db.SaveChanges();
            return newUrlItem.ID;
        }

        public int FindUrlByValue(string longUrl)
        {
            return db.UrlDetails.FirstOrDefault(item => item.Url.Equals(longUrl, StringComparison.OrdinalIgnoreCase))?.ID ?? -1;
        }

        public string GetUrl(int id)
        {
            return db.UrlDetails.FirstOrDefault(item => item.ID.Equals(id))?.Url;
        }
    }
}
