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
    
    public partial class CoreModule_PackageMaster
    {
        public int Id { get; set; }
        public string PackName { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string Usage { get; set; }
        public Nullable<decimal> MRP { get; set; }
        public Nullable<decimal> OfferPrice { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
