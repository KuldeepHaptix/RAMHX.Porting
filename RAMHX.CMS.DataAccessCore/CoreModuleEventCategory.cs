using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleEventCategory
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public bool? Active { get; set; }
        public int DisplayOrder { get; set; }
    }
}
