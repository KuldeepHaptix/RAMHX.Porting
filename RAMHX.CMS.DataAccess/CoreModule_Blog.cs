//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RAMHX.CMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class CoreModule_Blog
    {
        public int Id { get; set; }
        public Nullable<int> BlogCategoryId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogShortDesc { get; set; }
        public string LongDesc { get; set; }
        public bool Active { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public int DisplayOrder { get; set; }
    }
}
