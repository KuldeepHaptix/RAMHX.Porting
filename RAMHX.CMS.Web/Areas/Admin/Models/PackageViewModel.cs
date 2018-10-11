using RAMHX.CMS.DataAccess;
using RAMHX.CMS.DataAccess.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAMHX.CMS.Web.Areas.Admin.Models
{
    public class PackageViewModel
    {
        public List<cms_Pages> Pages { get; set; }

        public List<HtmlModule> HtmlModules { get; set; }

        public List<TemplateFieldsModel> TemplatesFilds { get; set; }

        public List<cms_PackageInstallations> PackageInstallations { get; set; }

        public string PublishingTarget { get; set; }
    }
}