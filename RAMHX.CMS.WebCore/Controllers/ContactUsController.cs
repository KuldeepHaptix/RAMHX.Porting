using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAMHX.CMS.RepositoryCore;

namespace RAMHX.CMS.WebCore.Controllers
{
    public class ContactUsController : Controller
    {
        //RepositoryCore.SendEmailRepo se = new RepositoryCore.SendEmailRepo();
        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            ContactUsModel conus = new ContactUsModel();
            return View(conus);
        }

        [HttpPost]
        public string SendEmail(string name, string contactno, string email, string category, string comments)
        {

            ContactUsModel conus = new ContactUsModel();

            if (!string.IsNullOrEmpty("email"))
            {
                //se.EmailMsg(name, contactno, email, category, comments);
            }

            return "[]";
        }

        [HttpPost]
        public string SendRequest(FormCollection fields)
        {
            try
            {
                string tempPath = Path.Combine("/EmailTemplates/" + fields["txtTemplate"] + ".html");
                if (System.IO.File.Exists(tempPath))
                {
                    string body = System.IO.File.ReadAllText(tempPath);
                    foreach (string item in fields.Keys)
                    {
                        body = body.Replace("#" + item + "#", fields[item]);
                    }
                    MailMessage message = new MailMessage();
                    message.Subject = fields["txtEmailSubject"];
                    message.IsBodyHtml = true;
                    message.Body = body;
                    message.To.Add(new MailAddress(fields["txtToEmail"]));
                    if (!string.IsNullOrEmpty(fields["txtCCEmail"]))
                    {
                        message.CC.Add(new MailAddress(fields["txtCCEmail"]));
                    }

                    if (!string.IsNullOrEmpty(fields["txtFromEmail"]))
                    {
                        message.From = new MailAddress(fields["txtFromEmail"]);
                    }

                    if (!string.IsNullOrEmpty(fields["txtBCCEmail"]))
                    {
                        message.Bcc.Add(new MailAddress(fields["txtBCCEmail"]));
                    }

                    //HttpFileCollectionBase files = Request.Files;
                    //for (int i = 0; i < files.Count; i++)
                    //{
                    //    HttpPostedFileBase file = files[i];
                    //    if (file.ContentLength > 1)
                    //    {
                    //        message.Attachments.Add(new Attachment(file.InputStream, Path.GetFileName(file.FileName)));
                    //    }
                    //}

                    //new SendEmailRepo().Send(message);
                }
                else
                {
                    logger.Warn("Contact Us does not have valid tempalte >> " + tempPath);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Contact Us Error - " + ex.Message, ex);
            }

            return "[]";
        }
    }
}