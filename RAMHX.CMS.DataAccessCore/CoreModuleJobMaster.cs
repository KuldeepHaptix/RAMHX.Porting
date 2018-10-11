using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleJobMaster
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string Specialization { get; set; }
        public int? CategoryId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
