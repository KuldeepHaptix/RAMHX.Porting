using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class AppForgotPasswordToken
    {
        public Guid TokenId { get; set; }
        public string CodeToken { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? ExpireDateTime { get; set; }
        public DateTime? CreatedDateTime { get; set; }
    }
}
