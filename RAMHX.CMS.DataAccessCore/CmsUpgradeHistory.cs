using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CmsUpgradeHistory
    {
        public string Script { get; set; }
        public string ReleasedDate { get; set; }
        public DateTime InstalledDate { get; set; }
        public string ReleasedNote { get; set; }
    }
}
