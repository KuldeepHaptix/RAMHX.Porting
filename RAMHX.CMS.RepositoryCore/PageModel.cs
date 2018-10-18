using RAMHX.CMS.DataAccessCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMHX.CMS.RepositoryCore
{
    public class PageModel
    {
        public CmsPages Page { get; set; }
        public List<HtmlModule> HtmlModules { get; set; }
        public List<AspNetRoles> PageRoles { get; set; }
        public List<CmsTemplates> PageTemplates { get; set; }
    }
}
