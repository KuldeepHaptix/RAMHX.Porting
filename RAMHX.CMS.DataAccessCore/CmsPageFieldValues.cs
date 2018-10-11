using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CmsPageFieldValues
    {
        public Guid PageId { get; set; }
        public Guid TemplateId { get; set; }
        public Guid TemplateFieldId { get; set; }
        public string FieldValue { get; set; }

        public CmsPages Page { get; set; }
        public CmsTemplates Template { get; set; }
        public CmsTemplateFields TemplateField { get; set; }
    }
}
