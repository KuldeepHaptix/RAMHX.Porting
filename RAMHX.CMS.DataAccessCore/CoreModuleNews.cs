using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleNews
    {
        public int Id { get; set; }
        public int? NewsCategoryId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsShortDesc { get; set; }
        public string LongDesc { get; set; }
        public bool? Active { get; set; }
        public DateTime? NewsDate { get; set; }
        public string AttachmentPath { get; set; }
        public int DisplayOrder { get; set; }
    }
}
