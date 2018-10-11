using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CmsPageTemplate
    {
        public Guid PageId { get; set; }
        public Guid TemplateId { get; set; }

        public CmsPages Page { get; set; }
        public CmsTemplates Template { get; set; }
    }
}
