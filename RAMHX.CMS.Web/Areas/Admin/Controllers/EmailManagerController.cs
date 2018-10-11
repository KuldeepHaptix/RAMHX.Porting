using RAMHX.CMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    public class EmailManagerController : Controller
    {
        [HttpPost, ValidateInput(false)]
        public JsonResult Send(FormCollection form)
        {
            try
            {
                SendEmailRepo em = new SendEmailRepo();
                em.SendEmail(form["subject"], form["toemail"], form["fromemail"], form["message"]);
                return Json(new { status = "sent" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "fail", error = ex.Message, stacktrace = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}