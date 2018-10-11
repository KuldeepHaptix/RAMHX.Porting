using HaptiX.CorpWeb.RAMHXWeb.Models;
using HaptiX.CorpWeb.RAMHXWeb.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaptiX.CorpWeb.RAMHXWeb.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult SendEmail(ContactModel con)
        {
            new Contact().ContactSendEmail(con);
            return Json(new { response = "success" });
            // return View();
        }

        // GET: Job Application
        [HttpPost]
        public ActionResult ApplyJob(FormCollection formData)
        {
            HttpPostedFileBase file = Request.Files["jobseeker-resume"];
            new JobRepo().SendMailForJobApplication(formData, file);
            return Json(new { response = "success" });
        }
    }
}