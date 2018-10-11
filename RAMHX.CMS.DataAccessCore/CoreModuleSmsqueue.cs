using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleSmsqueue
    {
        public int Id { get; set; }
        public string Smsnumber { get; set; }
        public string Smstext { get; set; }
        public bool IsSent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? SentDate { get; set; }
        public string SenderName { get; set; }
    }
}
