using RAMHX.CMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMHX.CMS.Repository
{
    public class PageModel
    {
        public cms_Pages Page { get; set; }
        public List<HtmlModule> HtmlModules { get; set; }
        public List<AspNetRole> PageRoles { get; set; }
        public List<cms_Templates> PageTemplates { get; set; }
    }
}
