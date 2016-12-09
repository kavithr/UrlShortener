using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortner.DAL
{
    public interface IDataAccessLayer
    {
        int Create(string longUrl);
        string GetUrl(int id);
        int FindUrlByValue(string longUrl);
    }
}
