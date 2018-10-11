using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    public class CacheController : Controller
    {
        // GET: Admin/Cache
        public string ClearAll()
        {
            Repository.CacheRepository.ClearAll();
            return "All Cleared Cache!";
        }
    }
}