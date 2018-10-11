using RAMHX.CMS.Infra;
using RAMHX.CMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace HaptiX.CorpWeb.RAMHXWeb.Repos
{
    public class EmailHelper
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void sendMail(string fromEmail, string subject, string mailBody, bool isBodyHtml, Attachment attachment)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(new AppConfiguration().GetGroupItemByItemAndGroupID(4, (int)Enums.AppConfigs.Groups.AppSettings).ItemDesc);
                mail.From = new MailAddress(fromEmail);
                mail.Subject = subject;
                mail.Body = mailBody;
                mail.IsBodyHtml = isBodyHtml;
                if (attachment != null)
                    mail.Attachments.Add(attachment);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = new AppConfiguration().GetGroupItemByItemAndGroupID(7, (int)Enums.AppConfigs.Groups.AppSettings).ItemDesc;
                smtp.Port = Convert.ToInt32(new AppConfiguration().GetGroupItemByItemAndGroupID(9, (int)Enums.AppConfigs.Groups.AppSettings).ItemDesc);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                (new AppConfiguration().GetGroupItemByItemAndGroupID(5, (int)Enums.AppConfigs.Groups.AppSettings).ItemDesc, new AppConfiguration().GetGroupItemByItemAndGroupID(6, (int)Enums.AppConfigs.Groups.AppSettings).ItemDesc);// Enter seders User name and password
                smtp.EnableSsl = new AppConfiguration().GetGroupItemByItemAndGroupID(8, (int)Enums.AppConfigs.Groups.AppSettings).ItemDesc.ToLower() == "no" ? false : true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                logger.Error("Error while sending Email ", ex);
            }
        }
    }
}