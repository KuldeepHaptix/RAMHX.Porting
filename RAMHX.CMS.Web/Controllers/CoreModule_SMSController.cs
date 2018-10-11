using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Controllers
{
    public class CoreModule_SMSController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();
        // GET: SMS
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult SendSMS(string numbers, string message)
        {
            foreach (var item in numbers.Split(',').ToArray())
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var sq = new CoreModule_SMSQueue();
                    sq.CreatedDate = DateTime.Now;
                    sq.SenderName = AppConfiguration.GetAppSettings("SenderName");
                    sq.IsSent = false;
                    sq.SMSNumber = item;
                    sq.SMSText = message;
                    db.CoreModule_SMSQueues.Add(sq);
                    db.SaveChanges();
                }
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetAllSMSHistory(DateTime start, DateTime end)
        {
            end = end.AddDays(1);
            var allsms = db.CoreModule_SMSQueues.Where(sms => sms.CreatedDate >= start && sms.CreatedDate <= end).OrderByDescending(sms => sms.CreatedDate).ToList();
            return Json(allsms, JsonRequestBehavior.AllowGet);
        }
    }
}