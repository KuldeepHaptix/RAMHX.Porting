using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}