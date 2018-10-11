using HaptiX.RAMHX.eDoctor.Infra;
using HaptiX.RAMHX.eDoctorBrand.Models;
using RAMHX.CMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaptiX.RAMHX.eDoctorBrand.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [HttpPost]
        public JsonResult SendQuery(FormCollection collection)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                SendEmailRepo sendEmail = new SendEmailRepo();
                Dictionary<string, string> paras = new Dictionary<string, string>();
                paras.Add(Contants.EMAILPARA_NAME, collection["name"]);
                paras.Add(Contants.EMAILPARA_MOBILE, collection["phone"]);
                paras.Add(Contants.EMAILPARA_EMAIL, collection["email"]);
                paras.Add(Contants.EMAILPARA_MESSAGE, collection["message"]);
                string message = sendEmail.GetEmailBody(Contants.HTMLMODULECODE_CONTACTUS_TAMPLATE, paras);
                sendEmail.SendEmail(Contants.CONTACTUS_SUBJECT, System.Configuration.ConfigurationManager.AppSettings[Contants.CONTACTUS_TO_EMAIL], System.Configuration.ConfigurationManager.AppSettings[Constatnts.SmtpUsername], message);

                response.Status = Contants.APISTATUSSUCCESS;
            }
            catch (Exception ex)
            {
                response.Status = Contants.APISTATUSERROR;
                response.Message =  ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}