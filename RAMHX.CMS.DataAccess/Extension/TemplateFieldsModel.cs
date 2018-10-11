using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMHX.CMS.DataAccess.Extension
{
    public class TemplateFieldsModel
    {
        public Guid TemplateId { get; set; }

        public Guid FieldId { get; set; }

        public string FieldName { get; set; }

        public string TemplateName { get; set; }

        public cms_Templates Template { get; set; }

        public cms_TemplateFields Field { get; set; }
    }
}
