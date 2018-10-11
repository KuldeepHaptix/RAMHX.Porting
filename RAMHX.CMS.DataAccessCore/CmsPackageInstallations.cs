using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CmsPackageInstallations
    {
        public Guid PackageId { get; set; }
        public string PackagePath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ComplatedDate { get; set; }
        public int? TotalPages { get; set; }
        public int? TotalModules { get; set; }
        public int? TotalTemplateFields { get; set; }
        public int? ProcPages { get; set; }
        public int? ProcModules { get; set; }
        public int? ProcTemplateFields { get; set; }
        public bool IsValidPackage { get; set; }
        public string ValidationErrors { get; set; }
    }
}
