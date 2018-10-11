using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RAMHX.CMS.Repository
{
    public enum CacheTypes
    {
        HtmlModule
    }

    public class CacheRepository
    {
        public static void Add(string key, object data, CacheTypes type)
        {
            int cacheMins = 0;
            int.TryParse(AppConfiguration.GetAppSettings("RAMHX.Cache.Duration"), out cacheMins);
            if (cacheMins == 0)
            {
                cacheMins = 5; //5 mins default
            }

            //-1 not to cache anything
            if (cacheMins > -1)
            {
                HttpContext.Current.Cache.Add(key + "_" + type.ToString(), data, null, DateTime.MaxValue, TimeSpan.FromMinutes(cacheMins), System.Web.Caching.CacheItemPriority.Normal, null);
            }
        }

        public static object Get(string key, CacheTypes type)
        {
            return HttpContext.Current.Cache.Get(key + "_" + type.ToString());
        }

        public static void Clear(CacheTypes type)
        {
            var keysToClear = (from System.Collections.DictionaryEntry dict in HttpContext.Current.Cache
                               let key = dict.Key.ToString()
                               where key.EndsWith("_" + type.ToString())
                               select key).ToList();


            foreach (var key in keysToClear)
            {
                HttpContext.Current.Cache.Remove(key);
            }
        }
        public static void Clear(string key, CacheTypes type)
        {
            HttpContext.Current.Cache.Remove(key + "_" + type.ToString());
        }

        public static void ClearAll()
        {
            var keysToClear = (from System.Collections.DictionaryEntry dict in HttpContext.Current.Cache
                               let key = dict.Key.ToString()
                               select key).ToList();


            foreach (var key in keysToClear)
            {
                HttpContext.Current.Cache.Remove(key);
            }
        }
    }
}
