using Infrastructure.Helper.Config;
using Infrastructure.Helper.Encryption;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cache
{
    public class RedisCache : IRedisCache
    {
        private IStringEncryption _stringEncryption;

        private string _url = string.Empty;
        private string _basekey = string.Empty;
        private IConfigCreatorHelper _configCreator;

        public RedisCache(IConfigCreatorHelper configCreator, IStringEncryption stringEncryption)
        {
            _stringEncryption = stringEncryption;
            _configCreator = configCreator;

            _url = configCreator.Get("Cache:CommonDB.String");
            _basekey = configCreator.Get("CommonDB.BaseKey.String");
        }

        public string Get(string key, bool compressValue = false)
        {
            try
            {
                key = _basekey + key;

                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    IDatabase db = redisConnection.GetDatabase();

                    var listString = db.StringGet(key);

                    if (string.IsNullOrWhiteSpace(listString)) return string.Empty;

                    if (compressValue)
                    {
                        listString = _stringEncryption.UnzipString(listString);
                    }

                    return listString;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public Dictionary<string, string> GetHashAll(string key)
        {
            try
            {
                key = _basekey + key;

                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    
                    Dictionary<string, string> returnValue = new Dictionary<string, string>();

                    var db = redisConnection.GetDatabase();
                    HashEntry[] redisData = db.HashGetAll(key);
                    if (redisData.Length > 0)
                    {
                        foreach (var data in redisData)
                        {
                            returnValue.Add(data.Name, _stringEncryption.UnzipString(data.Value));
                        }
                    }

                    return returnValue;
                }
            }
            catch (Exception)
            {
                return new Dictionary<string, string>();
            }
        }

        public string GetHash(string key, string hashField)
        {
            try
            {
                key = _basekey + key;

                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    string returnValue = string.Empty;

                    var db = redisConnection.GetDatabase();
                    var data = db.HashGet(key, hashField);
                    if (data.HasValue)
                    {
                        returnValue = _stringEncryption.UnzipString(data);
                    }

                    return returnValue;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public bool DeleteHash(string key, string hashField)
        {
            try
            {
                key = _basekey + key;

                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    string returnValue = string.Empty;

                    var db = redisConnection.GetDatabase();
                    return db.HashDelete(key, hashField);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteHashAll(string key)
        {
            try
            {
                key = _basekey + key;

                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    Dictionary<string, string> returnValue = new Dictionary<string, string>();

                    var db = redisConnection.GetDatabase();
                    HashEntry[] redisData = db.HashGetAll(key);
                    if (redisData.Length > 0)
                    {
                        foreach (var data in redisData)
                        {
                            db.HashDelete(key, data.Name);
                        }
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetHashWithoutEncrypt(string key, string hashField)
        {
            try
            {
                key = _basekey + key;

                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    string returnValue = string.Empty;

                    var db = redisConnection.GetDatabase();
                    var data = db.HashGet(key, hashField);
                    if (data.HasValue)
                        returnValue = data;

                    return returnValue;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public bool Set(string key, string value, int lifeTime = 1800, bool compressValue = false)
        {
            try
            {
                key = _basekey + key;

                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    IDatabase db = redisConnection.GetDatabase();

                    if (compressValue)
                    {
                        return db.StringSet(key, _stringEncryption.ZipString(value), TimeSpan.FromSeconds(lifeTime));
                    }
                    else
                    {
                        return db.StringSet(key, value, TimeSpan.FromSeconds(lifeTime));
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetWithoutExpired(string key, string value, bool compressValue = false)
        {
            try
            {
                key = _basekey + key;

                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    IDatabase db = redisConnection.GetDatabase();

                    if (compressValue)
                    {
                        return db.StringSet(key, _stringEncryption.ZipString(value), null);
                    }
                    else
                    {
                        return db.StringSet(key, value, null);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetHash(string key, string hashField, string value, int lifetime)
        {
            try
            {
                key = _basekey + key;

                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    var db = redisConnection.GetDatabase();
                    var returnValue = db.HashSet(
                        key,
                        hashField,
                        _stringEncryption.ZipString(value)
                    );
                    //db.KeyExpire(key, DateTime.Now.AddSeconds(lifetime));
                    //db.HashIncrement(key, "seconds", lifetime);

                    return returnValue;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetHashWithoutEncrypt(string key, string hashField, string value, int lifetime)
        {
            try
            {
                key = _basekey + key;

                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    var db = redisConnection.GetDatabase();
                    var returnValue = db.HashSet(
                        key,
                        hashField,
                        value
                    );
                    db.KeyExpire(key, DateTime.Now.AddSeconds(lifetime));

                    return returnValue;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Clear(string key)
        {
            try
            {
                key = _basekey + key;

                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    IDatabase db = redisConnection.GetDatabase();

                    return db.KeyExists(key) ? db.KeyDelete(key) : true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int countDateDiff(DateTime departureDate)
        {
            var dateTimeNow = DateTime.Now;
            var dateNow = new DateTime(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day);
            var timeSpan = departureDate - dateNow;

            return Convert.ToInt32(timeSpan.TotalDays);
        }

        private int setTimeOut(int dateDiff)
        {
            if (dateDiff <= 5) return 60;
            if (dateDiff > 6 && dateDiff <= 30) return 90;
            if (dateDiff > 31 && dateDiff <= 60) return 110;

            return 120;
        }

        public List<string> GetKeys(string pattern)
        {
            try
            {
                var listKeys = new List<string>();
                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    IServer server = redisConnection.GetServer(_configCreator.Get("Cache:CommonDB.Server.String"));
                    var redisKeys = server.Keys(0, pattern);

                    if (redisKeys == null)
                        return listKeys;

                    listKeys = redisKeys.Select(key => key.ToString()).ToList();
                }
                return listKeys;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public bool Deletes(string pattern)
        {
            try
            {
                var retValue = false;
                using (var redisConnection = ConnectionMultiplexer.Connect(_url))
                {
                    IServer server = redisConnection.GetServer(_configCreator.Get("Cache:CommonDB.Server.String"));
                    IDatabase db = redisConnection.GetDatabase();
                    var redisKeys = server.Keys(0, pattern);

                    if (server != null)
                    {
                        foreach (var key in redisKeys.Select(key => key.ToString()).ToList())
                        {
                            retValue = db.KeyDelete(key);
                        }
                    }
                }

                return retValue;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
