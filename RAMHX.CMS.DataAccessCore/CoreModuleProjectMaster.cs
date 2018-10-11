using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleProjectMaster
    {
        public int Id { get; set; }
        public string ProjName { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
