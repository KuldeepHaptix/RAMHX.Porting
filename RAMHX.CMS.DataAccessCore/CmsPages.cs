using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CmsPages
    {
        public CmsPages()
        {
            CmsPageFieldValues = new HashSet<CmsPageFieldValues>();
            CmsPageTemplate = new HashSet<CmsPageTemplate>();
            InverseParentPage = new HashSet<CmsPages>();
        }

        public Guid PageId { get; set; }
        public Guid? ParentPageId { get; set; }
        public int PageOrder { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public string Description { get; set; }
        public string PageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedByUserId { get; set; }
        public string PageTitle { get; set; }
        public string PageMetaKeywords { get; set; }
        public string PageMetaDescription { get; set; }
        public string PageLayoutPath { get; set; }
        public bool? ShowInNavigation { get; set; }

        public CmsPages ParentPage { get; set; }
        public ICollection<CmsPageFieldValues> CmsPageFieldValues { get; set; }
        public ICollection<CmsPageTemplate> CmsPageTemplate { get; set; }
        public ICollection<CmsPages> InverseParentPage { get; set; }
    }
}
