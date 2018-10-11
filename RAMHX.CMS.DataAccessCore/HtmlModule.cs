using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class HtmlModule
    {
        public Guid HtmlmoduleId { get; set; }
        public string HtmlModuleCode { get; set; }
        public string HtmlModuleName { get; set; }
        public string HtmlModuleDescription { get; set; }
        public string HtmlModuleHtml { get; set; }
        public string PageName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
    }
}
