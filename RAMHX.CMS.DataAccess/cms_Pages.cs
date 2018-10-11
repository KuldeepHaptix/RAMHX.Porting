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
    
    public partial class cms_Pages
    {
        public cms_Pages()
        {
            this.cms_PageFieldValues = new HashSet<cms_PageFieldValues>();
            this.cms_SubPages = new HashSet<cms_Pages>();
            this.cms_Templates = new HashSet<cms_Templates>();
        }
    
        public System.Guid PageID { get; set; }
        public Nullable<System.Guid> ParentPageID { get; set; }
        public int PageOrder { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public string Description { get; set; }
        public string PageUrl { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.Guid> CreatedByUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.Guid> ModifiedByUserId { get; set; }
        public string PageTitle { get; set; }
        public string PageMetaKeywords { get; set; }
        public string PageMetaDescription { get; set; }
        public string PageLayoutPath { get; set; }
        public bool ShowInNavigation { get; set; }
    
        public virtual ICollection<cms_PageFieldValues> cms_PageFieldValues { get; set; }
        public virtual ICollection<cms_Pages> cms_SubPages { get; set; }
        public virtual cms_Pages cms_ParentPage { get; set; }
        public virtual ICollection<cms_Templates> cms_Templates { get; set; }
    }
}