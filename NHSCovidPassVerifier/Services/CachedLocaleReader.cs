using System.Collections.Generic;
using System.IO;
using I18NPortable;
using I18NPortable.JsonReader;

namespace NHSCovidPassVerifier.Services
{
    public class CachedLocaleReader : ILocaleReader
    {
        private static Dictionary<string, string> _cachedDictionary;
        
        public Dictionary<string, string> Read(Stream stream)
        {
            return _cachedDictionary ??= new JsonKvpReader().Read(stream);
        }
    }
}