using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAMHX.CMS.DataAccessCore;
using RAMHX.CMS.RepositoryCore;

namespace RAMHX.CMS.WebCore.Areas.Admin.Controllers
{
    public class EmailManagerController : Controller
    {
        [Area("Admin")]
        [HttpPost]
        public JsonResult Send(FormCollection form)
        {
            try
            {
                //SendEmailRepo em = new SendEmailRepo();
                //em.SendEmail(form["subject"], form["toemail"], form["fromemail"], form["message"]);
                return Json(new { status = "sent" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "fail", error = ex.Message, stacktrace = ex.StackTrace });
            }
        }
    }
}