using System.Linq;


namespace UrlShortener.Repository
{
    public class BijectiveHashGenerator : IHashGenerator
    {
        public static readonly string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
        public static readonly int Base = Alphabet.Length;

        public string ConvertIdToHash(int id)
        {
            if (id == 0)
            {
                return Alphabet[0].ToString();
            };

            var s = string.Empty;

            while (id > 0)
            {
                s += Alphabet[id % Base];
                id = id / Base;
            }

            return string.Join(string.Empty, s.Reverse());
        }

        public int ConvertHashToId(string hash)
        {
            var index = 0;

            foreach (var letter in hash)
            {
                index = (index * Base) + Alphabet.IndexOf(letter);
            }

            return index;
        }
    }
}