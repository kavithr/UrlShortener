namespace UrlShortener.Repository
{
    public interface IHashGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string ConvertIdToHash(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        int ConvertHashToId(string hash);
    }
}