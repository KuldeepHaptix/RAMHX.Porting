using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleSliders
    {
        public int Id { get; set; }
        public string SliderCode { get; set; }
        public string SliderName { get; set; }
        public bool? Active { get; set; }
        public int DisplayOrder { get; set; }
    }
}
