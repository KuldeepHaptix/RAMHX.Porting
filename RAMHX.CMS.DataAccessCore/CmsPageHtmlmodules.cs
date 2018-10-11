using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CmsPageHtmlmodules
    {
        public Guid PageHtmlmoduleId { get; set; }
        public Guid? PageId { get; set; }
        public Guid? HtmlmoduleId { get; set; }
        public int OrderIndex { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedByUserId { get; set; }
    }
}
