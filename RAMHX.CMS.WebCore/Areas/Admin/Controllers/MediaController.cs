using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RAMHX.CMS.WebCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class MediaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}