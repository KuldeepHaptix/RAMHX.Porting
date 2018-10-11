using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAMHX.CMS.CoreModules.ForgotPassword.Areas.CoreModuleForgotPassword.Models
{
    public class ForgotPassword
    {
        public string TokenId { get; set; }
        public string CodeToken { get; set; }
        public string UserId { get; set; }
        public DateTime ExpireDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}