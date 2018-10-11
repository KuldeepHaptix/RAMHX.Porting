using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleEvents
    {
        public int Id { get; set; }
        public int? EventCategoryId { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public DateTime? EventDateTime { get; set; }
        public DateTime? EventRegistrationStartsOn { get; set; }
        public bool? Active { get; set; }
        public string PhotoUrl { get; set; }
        public string Location { get; set; }
        public DateTime? RegistrationEndOn { get; set; }
        public int DisplayOrder { get; set; }
    }
}
