using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAMHX.CMS.Web.Areas.Admin.Models
{
    public class PageHierarchy
    {
        /// <summary>
        /// Page ID
        /// </summary>
        public string PageID { get; set; }

        /// <summary>
        /// Page Nane
        /// </summary>
        public string PageName { get; set; }

       /// <summary>
       /// List of Children pages
       /// </summary>
        public List<PageHierarchy> ChildNood { get; set; }
        public PageHierarchy()
        {
            this.ChildNood = new List<PageHierarchy>();
        }

        /// <summary>
        /// Page Hierarchy
        /// </summary>
        /// <param name="child">child</param>
        public PageHierarchy(List<PageHierarchy> child)
        {
            this.ChildNood = new List<PageHierarchy>();
            // newchilds = child;
            this.ChildNood.AddRange(child);
        }
    }
}