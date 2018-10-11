using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleBlogCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool? Active { get; set; }
        public int DisplayOrder { get; set; }
    }
}
