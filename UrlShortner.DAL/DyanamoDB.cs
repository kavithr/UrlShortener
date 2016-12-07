using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortner.DAL
{
    public class DyanamoDB : IDataAccessLayer
    {
        public string CreateUrl(string url, int id)
        {
            return "Test";
        }

        public string GetUrl(string shortUrlHash)
        {
            throw new NotImplementedException();
        }
    }
}
