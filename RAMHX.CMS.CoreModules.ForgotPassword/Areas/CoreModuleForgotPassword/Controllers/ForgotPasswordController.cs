using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.CoreModules.ForgotPassword.Areas.CoreModuleForgotPassword.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: CoreModuleForgotPassword/ForgotPassword
        public JsonResult CheckUserName(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var result = db.AspNetUsers.FirstOrDefault(usr => usr.UserName == userName);
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SendCode(string UserId)
        {
            var user = db.AspNetUsers.FirstOrDefault(usr => usr.Id == UserId);
            var randomToken = RandomString(6);
            Guid tokenIdGuid = Guid.NewGuid();

            var forgotPasswordToken = new AppForgotPasswordToken()
            {
                TokenId = tokenIdGuid,
                UserId = Guid.Parse(user.Id),
                CodeToken = randomToken,
                CreatedDateTime = DateTime.Now,
                ExpireDateTime = DateTime.Now.AddMinutes(30)
            };
            db.AppForgotPasswordTokens.Add(forgotPasswordToken);
            db.SaveChanges();
            var sms = new CoreModule_SMSQueue()
            {
                CreatedDate = DateTime.Now,
                SenderName = AppConfiguration.GetAppSettings("SenderName"),
                SMSNumber = user.Mobile,
                SMSText = "Your Reset Password OTP: " + randomToken
            };
            db.CoreModule_SMSQueues.Add(sms);
            db.SaveChanges();
            return Json(new { result = forgotPasswordToken }, JsonRequestBehavior.AllowGet);
        }

        private static string RandomString(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                Random rand = new Random();
                var c = pool[rand.Next(0, pool.Length)];
                builder.Append(c);
            }
            return builder.ToString();
        }

        public JsonResult VerifiedToken(string UserId, string TokenId, string codeToken)
        {
            Guid userId = Guid.Parse(UserId);
            Guid tokenId = Guid.Parse(TokenId);
            var result = db.AppForgotPasswordTokens.FirstOrDefault(usr => usr.UserId == userId && usr.TokenId == tokenId && usr.ExpireDateTime > DateTime.Now && usr.CodeToken == codeToken);
            var expiredTokens = db.AppForgotPasswordTokens.Where(tkn => tkn.ExpireDateTime < DateTime.Now);
            db.AppForgotPasswordTokens.RemoveRange(expiredTokens);
            db.SaveChanges();
            if (result == null)
            {
                return Json(new { result = result, status = "fail", message = "Enter OTP is not valid or Expired" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = result, status = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}