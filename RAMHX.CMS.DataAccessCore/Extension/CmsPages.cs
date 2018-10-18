using System;
using System.Collections.Generic;
using System.Text;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CmsPages
    {
        public ICollection<CmsTemplates> CmsTemplates { get; set; }
        public string FullPageUrl { get; set; }
        public string FullItemPath { get; set; }
        
    }

    public partial class AspNetUsers
    {
        public ICollection<AspNetRoles> AspNetRoles { get; set; }
    }

    public partial class CmsTemplates
    {
        public ICollection<CmsPages> CmsPages { get; set; }
    }
    
}
