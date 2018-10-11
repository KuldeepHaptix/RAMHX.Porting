using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Models
{
    public class TemplatesHierarchy
    {
        /// <summary>
        /// Template ID
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// Template Nane
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// List of Children Template
        /// </summary>
        public List<TemplatesHierarchy> ChildNood { get; set; }
        public TemplatesHierarchy()
        {
            this.ChildNood = new List<TemplatesHierarchy>();
        }

        /// <summary>
        /// Template Hierarchy
        /// </summary>
        /// <param name="child">child</param>
        public TemplatesHierarchy(List<TemplatesHierarchy> child)
        {
            this.ChildNood = new List<TemplatesHierarchy>();
            // newchilds = child;
            this.ChildNood.AddRange(child);
        }
    }
}