using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CmsTemplates
    {
        public CmsTemplates()
        {
            CmsPageFieldValues = new HashSet<CmsPageFieldValues>();
            CmsPageTemplate = new HashSet<CmsPageTemplate>();
            CmsTemplateFields = new HashSet<CmsTemplateFields>();
        }

        public Guid TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateCode { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedByUserId { get; set; }

        public ICollection<CmsPageFieldValues> CmsPageFieldValues { get; set; }
        public ICollection<CmsPageTemplate> CmsPageTemplate { get; set; }
        public ICollection<CmsTemplateFields> CmsTemplateFields { get; set; }
    }
}
