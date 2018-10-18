using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RAMHX.CMS.RepositoryCore;

namespace RAMHX.CMS.WebCore.Areas.Admin.Controllers
{
    public class CacheController : Controller
    {
        public string ClearAll()
        {
            //CacheRepository cr = new CacheRepository();
            //cr.ClearAll();
            return "All Cleared Cache!";
        }
    }
}