using Microsoft.AspNetCore.Http;
using RAMHX.CMS.DataAccessCore;
using RAMHX.CMS.DataAccessCore.Extension;
using RAMHX.CMS.InfraCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RAMHX.CMS.RepositoryCore
{
    public class TemplatesRepository : BaseRepository
    {
        public TemplatesRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
        public CmsTemplates GetById(Guid id)
        {
            return dataContext.CmsTemplates.FirstOrDefault(te => te.TemplateId == id);
        }

        public List<CmsTemplates> GetAllTemplates()
        {
            return dataContext.CmsTemplates.ToList();
        }

        public List<TemplateFieldsModel> GetTemplateFields()
        {
            return (from tmp in dataContext.CmsTemplates
                    join fld in dataContext.CmsTemplateFields on tmp.TemplateId equals fld.TemplateId
                    select new TemplateFieldsModel
                    {
                        FieldName = fld.FieldName,
                        FieldId = fld.TemplateFieldId,
                        Field = fld,
                        Template = tmp,
                        TemplateId = tmp.TemplateId,
                        TemplateName = tmp.TemplateName
                    }).ToList();
        }

        public void SaveTemplateDetails(CmsTemplates tmp)
        {
            SiteContext st = new SiteContext(this.Context);
            try
            {
                CmsTemplates cmsTemplates = dataContext.CmsTemplates.FirstOrDefault(t => t.TemplateId == tmp.TemplateId);

                if (cmsTemplates == null)
                    cmsTemplates = new CmsTemplates() { TemplateId = Guid.NewGuid() };

                cmsTemplates.TemplateName = tmp.TemplateName;
                cmsTemplates.TemplateCode = tmp.TemplateCode;
                cmsTemplates.Description = tmp.Description;

                if (tmp.TemplateId == Guid.Empty)
                {
                    cmsTemplates.CreatedDate = DateTime.Now;
                    cmsTemplates.CreatedByUserId = st.CurrentUser_Guid;
                    dataContext.CmsTemplates.Add(cmsTemplates);
                }
                else
                {
                    cmsTemplates.ModifiedByUserId = st.CurrentUser_Guid;
                    cmsTemplates.ModifiedDate = tmp.ModifiedDate;
                }

                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public void SaveFieldTemplateDetails(CmsTemplateFields tmpField)
        {
            SiteContext st = new SiteContext(this.Context);
            CmsTemplateFields cmsTemplatesFields = new CmsTemplateFields();
            try
            {
                if (tmpField.TemplateFieldId != Guid.Empty)
                    cmsTemplatesFields = dataContext.CmsTemplateFields.First(t => t.TemplateFieldId == tmpField.TemplateFieldId);
                else
                    cmsTemplatesFields.TemplateFieldId = Guid.NewGuid();

                cmsTemplatesFields.FieldName = tmpField.FieldName;
                cmsTemplatesFields.FieldTypeId = tmpField.FieldTypeId;
                cmsTemplatesFields.FieldDisplayOrder = tmpField.FieldDisplayOrder;
                cmsTemplatesFields.DefaultValue = tmpField.DefaultValue;
                cmsTemplatesFields.TemplateId = tmpField.TemplateId;

                if (tmpField.TemplateFieldId != Guid.Empty)
                {
                    cmsTemplatesFields.ModifiedByUserId = st.CurrentUser_Guid;
                    cmsTemplatesFields.ModifiedDate = tmpField.ModifiedDate;
                }
                else
                {
                    cmsTemplatesFields.CreatedDate = DateTime.Now;
                    cmsTemplatesFields.CreatedByUserId = st.CurrentUser_Guid;

                }
                if (tmpField.TemplateFieldId == Guid.Empty)
                    dataContext.CmsTemplateFields.Add(cmsTemplatesFields);

                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public void UpdateFildDetail(CmsTemplateFields tmpField)
        {
            CmsTemplateFields cmsTemplatesFields = new CmsTemplateFields();
            cmsTemplatesFields = dataContext.CmsTemplateFields.First(t => t.TemplateFieldId == tmpField.TemplateFieldId);
            cmsTemplatesFields.FieldName = tmpField.FieldName;
            cmsTemplatesFields.TemplateFieldId = tmpField.TemplateFieldId;
            dataContext.SaveChanges();
        }

        public void CutCopyTemplateFiled(Guid pageid, Guid parentpageid, string mode)
        {
            var tempFiled = dataContext.CmsTemplateFields.Where(id => id.TemplateFieldId == pageid).FirstOrDefault();

            if (mode == "move_node")
            {
                tempFiled.TemplateId = parentpageid;
            }
            if (mode == "copy_node")
            {
                tempFiled.TemplateId = parentpageid;
                dataContext.CmsTemplateFields.Add(tempFiled);
            }
            dataContext.SaveChanges();
        }

        public CmsTemplates GetTemplatesById(string id)
        {
            Guid tmpid = Guid.Empty;
            Guid.TryParse(id, out tmpid);
            CmsTemplates cmt = new CmsTemplates();
            //dataContext.Configuration.ProxyCreationEnabled = false;
            return dataContext.CmsTemplates.Where(t => t.TemplateId == tmpid).FirstOrDefault();
        }

        public CmsTemplateFields GetTemplatesFieldById(string templateId, string fieldTemplateId)
        {
            Guid tmpid = Guid.Empty;
            Guid.TryParse(templateId, out tmpid);
            Guid tmpfilid = Guid.Empty;
            Guid.TryParse(fieldTemplateId, out tmpfilid);
            CmsTemplateFields cmtf = new CmsTemplateFields();
            //dataContext.Configuration.ProxyCreationEnabled = false;
            return cmtf = dataContext.CmsTemplateFields.Where(t => t.TemplateFieldId == tmpfilid && t.TemplateId == tmpid).FirstOrDefault();
        }

        public void DeleteChildren(Guid tempId)
        {
            var childrens = dataContext.CmsTemplateFields.Where(id => id.TemplateId == tempId).ToList();
            dataContext.CmsTemplateFields.RemoveRange(childrens);
            dataContext.SaveChanges();
            DeletePage(tempId);
        }

        public bool DeletePage(Guid tempId)
        {
            var deltemp = dataContext.CmsTemplates.Where(id => id.TemplateId == tempId).FirstOrDefault();
            dataContext.CmsTemplates.Remove(deltemp);
            dataContext.SaveChanges();
            return true;
        }

        //public List<cms_Templates> GetAllTemplates()
        //{
        //    return dataContext.cms_Templates.ToList();
        //}

        public void GetPageTemplates(Guid pageid)
        {
            //var curT
            var currentPage = dataContext.CmsPageFieldValues.Where(p => p.PageId == pageid);
            if (currentPage != null)
            {

                //dataContext.cms_Templates.Firs
                //dataContext.cms
            }
        }
    }
}
