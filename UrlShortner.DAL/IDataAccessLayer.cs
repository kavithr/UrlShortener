namespace UrlShortner.DAL
{
    public interface IDataAccessLayer
    {
        int Create(string longUrl);
        string GetUrl(int id);
        int FindUrlByValue(string longUrl);
    }
}
