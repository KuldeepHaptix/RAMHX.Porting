using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAMHX.CMS.Web.Areas.Admin.Models
{
    public class InstallerModel
    {
        public string SQLServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
    }
}