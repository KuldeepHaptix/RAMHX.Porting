using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using RAMHX.CMS.DataAccessCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMHX.CMS.RepositoryCore
{
    public class CacheRepository : BaseClass
    {
        public enum CacheTypes
        {
            HtmlModule
        }
        public CacheRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
        public static void Add(string key, object data, CacheTypes type)
        {
            int cacheMins = 0;
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["RAMHX.Cache.Duration"], out cacheMins);
            if (cacheMins == 0)
            {
                cacheMins = 5; //5 mins default
            }

            //-1 not to cache anything
            if (cacheMins > -1)
            {
                //_cache.Set<String>(key + "_" + type.ToString(), "", DateTime.MaxValue, TimeSpan.FromMinutes(cacheMins));
                //_cache.Set<Object>(key + "_" + type.ToString(), data,  DateTime.MaxValue, TimeSpan.FromMinutes(cacheMins));
            }
        }

        public  object Get(string key, CacheTypes type)
        {

            return _cache.Get(key + "_" + type.ToString());

        }

        public void Clear(CacheTypes type)
        {
            //var keysToClear = (from System.Collections.DictionaryEntry dict in HttpContext.Cache
            //let key = dict.Key.ToString()
            //where key.EndsWith("_" + type.ToString())
            //select key).ToList();


            //foreach (var key in keysToClear)
            //{
            //   _cache.Remove(key);
            //}
        }
        public void Clear(string key, CacheTypes type)
        {
            _cache.Remove(key + "_" + type.ToString());
        }

        public void ClearAll()
        {
            //var keysToClear = (from System.Collections.DictionaryEntry dict in _cache
            //                   let key = dict.Key.ToString()
            //                   select key).ToList();


            //foreach (var key in keysToClear)
            //{
            //    _cache.Remove(key);
            //}
        }
    }
}
