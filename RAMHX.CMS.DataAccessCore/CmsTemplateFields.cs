using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CmsTemplateFields
    {
        public CmsTemplateFields()
        {
            CmsPageFieldValues = new HashSet<CmsPageFieldValues>();
        }

        public Guid TemplateFieldId { get; set; }
        public string FieldName { get; set; }
        public int? FieldTypeId { get; set; }
        public int? FieldDisplayOrder { get; set; }
        public string DefaultValue { get; set; }
        public Guid? TemplateId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedByUserId { get; set; }
        public string DisplayName { get; set; }
        public string Notes { get; set; }

        public CmsTemplates Template { get; set; }
        public ICollection<CmsPageFieldValues> CmsPageFieldValues { get; set; }
    }
}
