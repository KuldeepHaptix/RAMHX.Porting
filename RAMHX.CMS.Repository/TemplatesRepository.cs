using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Web;
using RAMHX.CMS.DataAccess.Extension;

namespace RAMHX.CMS.Repository
{
    public class TemplatesRepository : BaseRepository
    {
        public cms_Templates GetById(Guid id)
        {
            return dataContext.cms_Templates.FirstOrDefault(te => te.TemplateId == id);
        }

        public List<cms_Templates> GetAllTemplates()
        {
            return dataContext.cms_Templates.ToList();
        }

        public List<TemplateFieldsModel> GetTemplateFields()
        {
            return (from tmp in dataContext.cms_Templates
                    join fld in dataContext.cms_TemplateFields on tmp.TemplateId equals fld.TemplateId
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

        public void SaveTemplateDetails(cms_Templates tmp)
        {
            try
            {
                cms_Templates cmsTemplates = dataContext.cms_Templates.FirstOrDefault(t => t.TemplateId == tmp.TemplateId);

                if (cmsTemplates == null)
                    cmsTemplates = new cms_Templates() { TemplateId = Guid.NewGuid() };

                cmsTemplates.TemplateName = tmp.TemplateName;
                cmsTemplates.TemplateCode = tmp.TemplateCode;
                cmsTemplates.Description = tmp.Description;

                if (tmp.TemplateId == Guid.Empty)
                {
                    cmsTemplates.CreatedDate = DateTime.Now;
                    cmsTemplates.CreatedByUserId = SiteContext.CurrentUser_Guid;
                    dataContext.cms_Templates.Add(cmsTemplates);
                }
                else
                {
                    cmsTemplates.ModifiedByUserId = SiteContext.CurrentUser_Guid;
                    cmsTemplates.ModifiedDate = tmp.ModifiedDate;
                }

                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public void SaveFieldTemplateDetails(cms_TemplateFields tmpField)
        {
            cms_TemplateFields cmsTemplatesFields = new cms_TemplateFields();
            try
            {
                if (tmpField.TemplateFieldId != Guid.Empty)
                    cmsTemplatesFields = dataContext.cms_TemplateFields.First(t => t.TemplateFieldId == tmpField.TemplateFieldId);
                else
                    cmsTemplatesFields.TemplateFieldId = Guid.NewGuid();

                cmsTemplatesFields.FieldName = tmpField.FieldName;
                cmsTemplatesFields.FieldTypeId = tmpField.FieldTypeId;
                cmsTemplatesFields.FieldDisplayOrder = tmpField.FieldDisplayOrder;
                cmsTemplatesFields.DefaultValue = tmpField.DefaultValue;
                cmsTemplatesFields.TemplateId = tmpField.TemplateId;

                if (tmpField.TemplateFieldId != Guid.Empty)
                {
                    cmsTemplatesFields.ModifiedByUserId = SiteContext.CurrentUser_Guid;
                    cmsTemplatesFields.ModifiedDate = tmpField.ModifiedDate;
                }
                else
                {
                    cmsTemplatesFields.CreatedDate = DateTime.Now;
                    cmsTemplatesFields.CreatedByUserId = SiteContext.CurrentUser_Guid;

                }
                if (tmpField.TemplateFieldId == Guid.Empty)
                    dataContext.cms_TemplateFields.Add(cmsTemplatesFields);

                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public void UpdateFildDetail(cms_TemplateFields tmpField)
        {
            cms_TemplateFields cmsTemplatesFields = new cms_TemplateFields();
            cmsTemplatesFields = dataContext.cms_TemplateFields.First(t => t.TemplateFieldId == tmpField.TemplateFieldId);
            cmsTemplatesFields.FieldName = tmpField.FieldName;
            cmsTemplatesFields.TemplateFieldId = tmpField.TemplateFieldId;
            dataContext.SaveChanges();
        }

        public void CutCopyTemplateFiled(Guid pageid, Guid parentpageid, string mode)
        {
            var tempFiled = dataContext.cms_TemplateFields.Where(id => id.TemplateFieldId == pageid).FirstOrDefault();

            if (mode == "move_node")
            {
                tempFiled.TemplateId = parentpageid;
            }
            if (mode == "copy_node")
            {
                tempFiled.TemplateId = parentpageid;
                dataContext.cms_TemplateFields.Add(tempFiled);
            }
            dataContext.SaveChanges();
        }

        public cms_Templates GetTemplatesById(string id)
        {
            Guid tmpid = Guid.Empty;
            Guid.TryParse(id, out tmpid);
            cms_Templates cmt = new cms_Templates();
            dataContext.Configuration.ProxyCreationEnabled = false;
            return dataContext.cms_Templates.Where(t => t.TemplateId == tmpid).FirstOrDefault();
        }

        public cms_TemplateFields GetTemplatesFieldById(string templateId, string fieldTemplateId)
        {
            Guid tmpid = Guid.Empty;
            Guid.TryParse(templateId, out tmpid);
            Guid tmpfilid = Guid.Empty;
            Guid.TryParse(fieldTemplateId, out tmpfilid);
            cms_TemplateFields cmtf = new cms_TemplateFields();
            dataContext.Configuration.ProxyCreationEnabled = false;
            return cmtf = dataContext.cms_TemplateFields.Where(t => t.TemplateFieldId == tmpfilid && t.TemplateId == tmpid).FirstOrDefault();
        }

        public void DeleteChildren(Guid tempId)
        {
            var childrens = dataContext.cms_TemplateFields.Where(id => id.TemplateId == tempId).ToList();
            dataContext.cms_TemplateFields.RemoveRange(childrens);
            dataContext.SaveChanges();
            DeletePage(tempId);
        }

        public bool DeletePage(Guid tempId)
        {
            var deltemp = dataContext.cms_Templates.Where(id => id.TemplateId == tempId).FirstOrDefault();
            dataContext.cms_Templates.Remove(deltemp);
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
            var currentPage = dataContext.cms_PageFieldValues.Where(p => p.PageId == pageid);
            if (currentPage != null)
            {

                //dataContext.cms_Templates.Firs
                //dataContext.cms
            }
        }
    }
}
