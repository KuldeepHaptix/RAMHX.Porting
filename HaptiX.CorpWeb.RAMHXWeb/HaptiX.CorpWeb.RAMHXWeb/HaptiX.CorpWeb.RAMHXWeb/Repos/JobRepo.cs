using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaptiX.CorpWeb.RAMHXWeb.Models;
using RAMHX.CMS.DataAccess.Extension;
using RAMHX.CMS.Web;
using System.Web.Mvc;
using System.Text;
using System.Net.Mail;
using System.IO;

namespace HaptiX.CorpWeb.RAMHXWeb.Repos
{
    public class JobRepo
    {
        public List<JobModel> GetJobs()
        {
            List<JobModel> jobs = new List<JobModel>();
            var jobpage = SiteContext.Pages.FirstOrDefault(p => p.PageCode == "Jobs");
            if (jobpage != null)
            {
                foreach (var item in jobpage.cms_SubPages)
                {

                    var FieldValues = item.FieldValues();
                    var jt = FieldValues.First(fv => fv.FieldName == "JobTitle").FieldValue;
                    var jd = string.Empty;
                    if (FieldValues.FirstOrDefault(fv => fv.FieldName == "JobDescription") != null)
                    {
                        jd = FieldValues.FirstOrDefault(fv => fv.FieldName == "JobDescription").FieldValue;
                    }
                    jobs.Add(new JobModel { JobTitle = jt, JobDescription = jd });
                }
            }
            return jobs;
        }


        public void SendMailForJobApplication(FormCollection formData, HttpPostedFileBase file)
        {
            StringBuilder sb = new StringBuilder();
            string fromEmail = formData["jobseeker-email"];
            string jobTitle = formData["jobseeker-title"];

            sb.AppendLine("<table>");
            appendTr(ref sb, "Name", formData["jobseeker-name"]);
            appendTr(ref sb, "Contact", formData["jobseeker-contact"]);
            appendTr(ref sb, "Email", fromEmail);
            appendTr(ref sb, "Job Title", jobTitle);
            appendTr(ref sb, "Message", formData["jobseeker-message"]);
            sb.AppendLine("</table>");

            Attachment resume = null;

            if (file != null)
            {
                byte[] data;
                using (Stream inputStream = file.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }

                resume = new Attachment(new MemoryStream(data), file.FileName);
            }

            new EmailHelper().sendMail(fromEmail, "Job Application for " + jobTitle, sb.ToString(), true, resume);
        }

        private void appendTr(ref StringBuilder sb, string name, string value)
        {
            sb.AppendLine("<tr>");
            sb.AppendLine("<td>");
            sb.AppendLine(name);
            sb.AppendLine("</td>");
            sb.AppendLine("<td>");
            sb.AppendLine(value);
            sb.AppendLine("</td>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
        }
    }
}