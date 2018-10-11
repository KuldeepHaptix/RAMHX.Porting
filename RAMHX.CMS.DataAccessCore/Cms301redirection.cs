using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class Cms301redirection
    {
        public Guid Rid { get; set; }
        public string FromUrl { get; set; }
        public string ToUrl { get; set; }
        public bool Active { get; set; }
    }
}
