using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleTestimonialMaster
    {
        public int Id { get; set; }
        public string Experience { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public int? CategoryId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
