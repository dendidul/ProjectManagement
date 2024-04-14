using CookieManager;

namespace Web.Utils
{
    public class CookieManager
    {

        private ICookie _cookie;

        public CookieManager(ICookie cookie)
        {
            _cookie = cookie;
        }

        public string GetCookie(string key)
        {
            //return _cookie.Get(key);
            if (key == "UserId")
            {
                var cookie = _cookie.Get(key);
                if (cookie == "")
                {
                    return "0";
                }
                else
                {
                    return _cookie.Get(key);
                }
            }
            else
            {
                return _cookie.Get(key);
            }

        }

        
        public void SetCookie(string key, string value)
        {
            CookieOptions option = new CookieOptions();
            //option.Expires = DateTime.Now.AddDays(Convert.ToInt32(CookieTimeOut()));
            option.Expires = DateTime.Now.AddDays(1);
            _cookie.Set(key, value, option);
            //_cookie.Set()

        }

        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void RemoveCookie(string key)
        {
            _cookie.Remove(key);
            //Response.Cookies.Delete(key);
        }
    }
}
