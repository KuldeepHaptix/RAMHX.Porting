using Quartz;
using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace RAMHX.CMS.Web.Schedulers
{
    public class SMSProcess : IJob
    {
        public static bool SMSProcessing = false;
        /// <summary>
        /// Keep Alive
        /// </summary>
        /// <param name="context">schedule job context</param>
        public void Execute(IJobExecutionContext context)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info("SMSProcess > Execute > SMS.Enabled:" + AppConfiguration.GetAppSettings("SMS.Enabled") + ", SMS PROCESSING:" + (SMSProcessing ? "YES" : "NO"));
            if (!SMSProcessing && AppConfiguration.GetAppSettings("SMS.Enabled") == "1")
            {
                SendSMSProcess();
            }
        }

        public static void SendSMSProcess()
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            SMSProcessing = true;
            logger.Info("SMSProcess > Started");
            try
            {
                DatabaseEntities dbEntity = new DatabaseEntities();
                var ssms = dbEntity.CoreModule_SMSQueues.Where(ss => ss.IsSent == false).Take(50).ToList();
                foreach (var item in ssms)
                {
                    try
                    {
                        item.SMSText = item.SMSText.Replace("#", string.Empty).Replace("&", string.Empty);
                        string url = AppConfiguration.GetAppSettings("SMS.WebUrl") + "username=" + AppConfiguration.GetAppSettings("SMS.username") + "&message=" + item.SMSText + "&sendername=" + item.SenderName + "&smstype=TRANS&numbers=" + item.SMSNumber + "&apikey=" + AppConfiguration.GetAppSettings("SMS.Key");

                        // Create a request for the URL. 
                        WebRequest request = WebRequest.Create(url);
                        // If required by the server, set the credentials.
                        request.Credentials = CredentialCache.DefaultCredentials;
                        // Get the response.
                        WebResponse response = request.GetResponse();
                        // Display the status.
                        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                        // Get the stream containing content returned by the server.
                        Stream dataStream = response.GetResponseStream();
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader = new StreamReader(dataStream);
                        // Read the content.
                        string responseFromServer = reader.ReadToEnd();
                        // Display the content.
                        // Clean up the streams and the response.
                        reader.Close();
                        response.Close();

                        item.SentDate = DateTime.Now;
                        item.IsSent = true;
                        dbEntity.SaveChanges();
                    }
                    catch (Exception ex) { logger.Error("SMS PROCCESS ERROR", ex); }
                }
            }
            catch (Exception ex) { logger.Error("SMS PROCCESS ERROR", ex); }
            SMSProcessing = false;
            logger.Info("SMSProcess > Ended");
        }
    }
}