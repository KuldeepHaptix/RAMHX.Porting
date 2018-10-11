using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MediaController : BaseController
    {
        // GET: Admin/Media
        public ActionResult Index()
        {
            return View();
        }
    }
}