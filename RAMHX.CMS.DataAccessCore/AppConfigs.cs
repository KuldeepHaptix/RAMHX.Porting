using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class AppConfigs
    {
        public int GroupId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDesc { get; set; }
        public bool? IsActive { get; set; }
        public string ShortDesc { get; set; }
        public string ItemCode { get; set; }
    }
}
