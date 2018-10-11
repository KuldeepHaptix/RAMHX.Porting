using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace RAMHX.CMS.Repository
{
    public class SendEmailRepo : BaseRepository
    {

        public void EmailMsg(string name, string contactno, string email, string category, string comments)
        {
            ContactUsModel conus = new ContactUsModel();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            sb.Append("<td>");
            sb.Append("Name:");
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append(name);
            sb.Append("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.Append("<td>");
            sb.Append("Contact No:");
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append(contactno);
            sb.Append("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.Append("<td>");
            sb.Append("Email ID:");
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append(email);
            sb.Append("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.Append("<td>");
            sb.Append("Category:");
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append(category);
            sb.Append("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.Append("<td>");
            sb.Append("Comments:");
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append(comments);
            sb.Append("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");

            MailMessage message = new MailMessage()
            {
                Subject = "Contact Us",
                Body = sb.ToString(),
                IsBodyHtml = true
            };

            try
            {
                message.From = new MailAddress(email);
                log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Warn("Send Contact Us email from in try catch - " + email);
            }
            catch { }
            
            Send(AppConfiguration.GetAppSettings("ContactUs.To.EmailID").ToString(), message);

        }


        public async Task Send(string recipientEmail, MailMessage msg)
        {

            //MailMessage msg = new MailMessage();

            msg.To.Clear();
            msg.To.Add(recipientEmail);
            if (msg.From == null)
            {
                msg.From = new MailAddress(AppConfiguration.GetAppSettings("From.EmailID"));
                log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Warn("Send Contact Us email from - " + AppConfiguration.GetAppSettings("From.EmailID"));
            }

            var client = new System.Net.Mail.SmtpClient();

            
            client.Host = AppConfiguration.GetAppSettings("SmtpServer").ToString();
            client.Port = Convert.ToInt32(AppConfiguration.GetAppSettings("SmtpPort"));
            client.EnableSsl = Convert.ToBoolean(AppConfiguration.GetAppSettings("SmtpRequiredSSL"));
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(AppConfiguration.GetAppSettings("SmtpUsername").ToString(), AppConfiguration.GetAppSettings("SmtpPassword").ToString());
            client.Timeout = 2000000;

            client.SendCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    logger.Error(e.Error);
                }

                client.Dispose();
                msg.Dispose();
            };

            await Task.Run(() =>
            {
                try
                {
                    client.Send(msg);
                }
                catch (Exception e)
                {
                    log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    logger.Error(e);
                }
            });

        }

        public void SendEmail(string subject, string toEmail, string fromEmail, string emailMessage)
        {

            MailMessage message = new MailMessage()
            {
                Subject = subject,
                Body = emailMessage,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);
            message.From = new MailAddress(fromEmail);

            Send(message);

        }

        public void Send(MailMessage msg)
        {
            var client = new System.Net.Mail.SmtpClient();
            client.Host = AppConfiguration.GetAppSettings("SmtpServer").ToString();
            client.Port = Convert.ToInt32(AppConfiguration.GetAppSettings("SmtpPort"));
            client.EnableSsl = Convert.ToBoolean(AppConfiguration.GetAppSettings("SmtpRequiredSSL"));
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(AppConfiguration.GetAppSettings("SmtpUsername").ToString(), AppConfiguration.GetAppSettings("SmtpPassword").ToString());
            client.Timeout = 2000000;

            try
            {
                client.Send(msg);
            }
            catch (Exception e)
            {
                log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Error(e);
            }
        }

        public string GetEmailBody(string htmlModuleCode, Dictionary<string, string> parameters)
        {
            var mainHtml = this.dataContext.HtmlModules.FirstOrDefault(h => h.HtmlModuleCode == htmlModuleCode);
            foreach (var item in parameters)
            {
                mainHtml.HtmlModuleHTML = mainHtml.HtmlModuleHTML.Replace(item.Key, item.Value);
            }
            return mainHtml.HtmlModuleHTML;
        }
    }
}
