using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleBlogs
    {
        public int Id { get; set; }
        public int? BlogCategoryId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogShortDesc { get; set; }
        public string LongDesc { get; set; }
        public bool? Active { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public int DisplayOrder { get; set; }
    }
}
