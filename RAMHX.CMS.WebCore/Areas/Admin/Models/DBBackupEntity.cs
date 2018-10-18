using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMHX.CMS.WebCore.Areas.Admin.Models
{
    public class DBBackupEntity
    {
        public int SRNO { get; set; }
        public string FileName { get; set; }
        public string DateTime { get; set; }
        public string FullPath { get; set; }
    }
}
