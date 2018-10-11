using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleFaqmaster
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int? CategoryId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
