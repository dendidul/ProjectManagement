using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cache
{
    public interface IRedisCache
    {
        string Get(string key, bool compressValue = false);
        Dictionary<string, string> GetHashAll(string key);
        string GetHash(string key, string hashField);
        string GetHashWithoutEncrypt(string key, string hashField);
        bool Set(string key, string value, int lifeTime = 1800, bool compressValue = false);
        bool SetWithoutExpired(string key, string value, bool compressValue = false);
        bool SetHash(string key, string hashField, string value, int lifetime);
        bool SetHashWithoutEncrypt(string key, string hashField, string value, int lifetime);
        bool Clear(string key);
        List<string> GetKeys(string pattern);
        bool Deletes(string pattern);
        bool DeleteHash(string key, string hashField);
        bool DeleteHashAll(string key);
    }
}
