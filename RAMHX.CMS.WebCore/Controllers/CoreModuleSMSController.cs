using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAMHX.CMS.DataAccessCore;
using RAMHX.CMS.RepositoryCore;

namespace RAMHX.CMS.WebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoreModuleSMSController : ControllerBase
    {
        private DatabaseEntities db = new DatabaseEntities();
        // GET: SMS
        public IActionResult Index()
        {
            return new ViewResult();
        }

        [HttpPost]
        [Authorize]
        public JsonResult SendSMS(string numbers, string message)
        {
            foreach (var item in numbers.Split(',').ToArray())
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var sq = new CoreModuleSmsqueue();
                    sq.CreatedDate = DateTime.Now;
                    sq.SenderName = AppConfiguration.GetAppSettings("SenderName");
                    sq.IsSent = false;
                    sq.Smsnumber = item;
                    sq.Smstext = message;
                    db.CoreModuleSmsqueue.Add(sq);
                    db.SaveChanges();
                }
            }
            return new JsonResult(new{data="success"} );
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetAllSMSHistory(DateTime start, DateTime end)
        {
            end = end.AddDays(1);
            var allsms = db.CoreModuleSmsqueue.Where(sms => sms.CreatedDate >= start && sms.CreatedDate <= end).OrderByDescending(sms => sms.CreatedDate).ToList();
            return new JsonResult(allsms);
        }
    }
}