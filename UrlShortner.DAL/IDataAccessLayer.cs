using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortner.DAL
{
    public interface IDataAccessLayer
    {
        string CreateUrl(string url, int id);
        string GetUrl(string shortUrlHash);
    }
}
