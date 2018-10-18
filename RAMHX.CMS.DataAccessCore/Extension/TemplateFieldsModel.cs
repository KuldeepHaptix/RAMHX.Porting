using System;
using System.Collections.Generic;
using System.Text;

namespace RAMHX.CMS.DataAccessCore.Extension
{
    public class TemplateFieldsModel
    {
        public Guid TemplateId { get; set; }

        public Guid FieldId { get; set; }

        public string FieldName { get; set; }

        public string TemplateName { get; set; }

        public CmsTemplates Template { get; set; }

        public CmsTemplateFields Field { get; set; }
    }
}
