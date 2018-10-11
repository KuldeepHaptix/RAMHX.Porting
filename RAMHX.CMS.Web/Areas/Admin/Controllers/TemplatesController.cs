using Newtonsoft.Json;
using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Repository;
using RAMHX.CMS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TemplatesController : BaseController
    {
        TemplatesRepository tempsRepo = new TemplatesRepository();

        PageRepository pageRepo = new PageRepository();
        // GET: Admin/Templates
        [Authorize]
        public ActionResult Index()
        {
            var RootPages = tempsRepo.GetAllTemplates();
            List<TemplatesHierarchy> Listpagehairarchy = new List<TemplatesHierarchy>();
            foreach (var item in RootPages)
            {
                TemplatesHierarchy pgh = new TemplatesHierarchy();
                pgh.TemplateId = item.TemplateId.ToString();
                pgh.TemplateName = item.TemplateName;
                Listpagehairarchy.Add(pgh);
            }

            ViewBag.FieldTypes = new AppConfiguration().GetFieldTypeSelectList();

            return View(Listpagehairarchy);
        }

        [Authorize]
        public ActionResult TemplateFields()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetFieldTypes()
        {
            DatabaseEntities db = new DatabaseEntities();
            var fieldTypes = db.app_Configs.Where(ft => ft.GroupId == 2).ToList();
            return Json(fieldTypes, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetFieldTemplate(Guid templateId)
        {
            DatabaseEntities db = new DatabaseEntities();
            var fieldTemplates = db.cms_TemplateFields.Where(tem => tem.TemplateId == templateId).ToList();
            var fieldtemplate = (from field in fieldTemplates
                                 select new
                                 {
                                     field.TemplateFieldId,
                                     field.FieldName,
                                     field.FieldTypeId,
                                     field.FieldDisplayOrder,
                                     field.DefaultValue,
                                     field.DisplayName,
                                     field.Notes
                                 });
            return Json(fieldtemplate, JsonRequestBehavior.AllowGet);
                                 
        }

        /// <summary>
        /// Get root node
        /// </summary>
        /// <returns>Return Root node</returns>
        public JsonResult GetRootTemplate()
        {
            JsTreeItem rootNode = new JsTreeItem();
            // rootNode.attr = new JsTreeAttribute();
            rootNode.text = "Templates";
            rootNode.id = Guid.Empty.ToString();
            return Json(rootNode, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TemplateList(Guid pageId)
        {
            var listTemplate = tempsRepo.GetAllTemplates();
            if (pageId != Guid.Empty)
            {
                var list = pageRepo.GetPage(pageId).cms_Templates.Select(temp => temp.TemplateId).ToList();
                listTemplate = listTemplate.Where(tmpl => !list.Contains(tmpl.TemplateId)).ToList();
            }
            return View(listTemplate);
        }

        public JsonResult GetAllTemplates()
        {
            List<JsTreeItem> rootNode = new List<JsTreeItem>();
            foreach (var item in tempsRepo.GetAllTemplates())
            {
                JsTreeItem nood = new JsTreeItem();
                nood.id = item.TemplateId.ToString();//.Replace("{", "").Replace("}", ""); ;
                nood.text = item.TemplateName;
                nood.data = item.TemplateName;
                rootNode.Add(nood);
            }

            return Json(rootNode, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Get Child nodes
        /// </summary>
        /// <param name="pageid">Parent Page id</param>
        /// <returns>Returns childerns</returns>
        public JsonResult GetTemplatesField(string templateid)
        {
            //templateid = templateid.Split('-').GetValue(1).ToString();
            Guid tid = Guid.Empty;
            if (Guid.TryParse(templateid, out tid))
            //if (!string.IsNullOrEmpty(templateid) && Convert.ToInt32(templateid) > 0)
            {
                var template = tempsRepo.GetById(tid);
                List<JsTreeItem> rootNode = new List<JsTreeItem>();
                if (template != null)
                {
                    foreach (var item in template.cms_TemplateFields)
                    {
                        JsTreeItem nood = new JsTreeItem();
                        nood.id = item.TemplateFieldId.ToString();//.Replace("{", "").Replace("}", ""); ;
                        nood.text = item.FieldName;
                        nood.data = item.FieldName;
                        rootNode.Add(nood);
                    }
                }

                return Json(rootNode, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var RootTemplates = tempsRepo.GetAllTemplates();
                List<JsTreeItem> rootNode = new List<JsTreeItem>();
                foreach (var item in RootTemplates)
                {
                    JsTreeItem nood = new JsTreeItem();
                    nood.id = item.TemplateId.ToString();//.Replace("{", "").Replace("}", ""); ;
                    nood.text = item.TemplateName;
                    nood.data = item.TemplateName;
                    rootNode.Add(nood);
                }
                return Json(rootNode, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        public JsonResult SaveTemplate(cms_Templates tmp)
        {
            tempsRepo.SaveTemplateDetails(tmp);
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult SaveFieldTemplate(cms_TemplateFields tmpField)
        {
            tempsRepo.SaveFieldTemplateDetails(tmpField);
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTemplatesById(string id)
        {
            return Json(tempsRepo.GetTemplatesById(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTemplatesFieldById(string templateid, string fieldTemplateId)
        {
            return Json(tempsRepo.GetTemplatesFieldById(templateid, fieldTemplateId), JsonRequestBehavior.AllowGet);
        }

        public int DeleteChildren(string tempid)
        {
            try
            {
                tempsRepo.DeleteChildren(Guid.Parse(tempid));
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public string GetSplitedId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return id = id.Split('-').GetValue(1).ToString();

            }
            return id;
        }

        //public JsonResult SaveTemplateAndField(string tmpfldid, string tmpid)
        //{

        //    foreach (var item in tmpid.Split(','))
        //    {
        //        tempsRepo.SaveTemplateDetails(Convert.ToInt32(tmpfldid), Convert.ToInt32(item));
        //    }
        //    return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult RenameTemplate(string templateDetails, string templatefieldid, string templateid)
        {
            //var temp = templateid.Split('-');
            Guid tid = Guid.Empty;
            Guid.TryParse(templatefieldid, out tid);
            if (tid != Guid.Empty)
            {
                // edit mode
                if (tid != Guid.Empty)
                {
                    cms_Templates newtemplate = new cms_Templates();
                    newtemplate.TemplateName = templateDetails;
                    newtemplate.TemplateCode = templateDetails;
                    newtemplate.Description = templateDetails;
                    newtemplate.TemplateId = tid;
                    tempsRepo.SaveTemplateDetails(newtemplate);
                }
                else
                {
                    cms_TemplateFields tmpField = new cms_TemplateFields();
                    tmpField.TemplateFieldId = tid;
                    tmpField.FieldName = templateDetails;
                    tempsRepo.UpdateFildDetail(tmpField);
                }
            }
            else
            {
                // create mode
                // var pid = parentid.Split('-');
                //if (Convert.ToInt16(pid[0]) == 0)
                Guid pid = Guid.Empty;
                Guid.TryParse(templateid, out pid);
                if (pid != Guid.Empty)
                {
                    // create template filed
                    cms_TemplateFields tmpField = new cms_TemplateFields();
                    tmpField.TemplateFieldId = Guid.Empty;
                    tmpField.TemplateId = pid;
                    tmpField.FieldName = templateDetails;
                    tmpField.FieldTypeId = 1;
                    tempsRepo.SaveFieldTemplateDetails(tmpField);
                }
                else
                {
                    // create template
                    cms_Templates newtemplate = new cms_Templates();
                    newtemplate.TemplateName = templateDetails;
                    newtemplate.TemplateCode = templateDetails;
                    newtemplate.Description = templateDetails;
                    newtemplate.TemplateId = Guid.Empty;
                    tempsRepo.SaveTemplateDetails(newtemplate);
                }
                //else
                //{
                //    return Json(new { status = "failler" }, JsonRequestBehavior.AllowGet);
                //}
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CutCopyTemplate(string pageid, string parentpageid, string mode)
        {
            if (parentpageid != "0-0")
            {
                //var ppid = parentpageid.Split('-');
                //var pid = pageid.Split('-');
                Guid ppid = Guid.Empty;
                Guid.TryParse(parentpageid, out ppid);
                Guid pid = Guid.Empty;
                Guid.TryParse(pageid, out pid);
                tempsRepo.CutCopyTemplateFiled(pid, ppid, mode);
                //tempsRepo.CutCopyTemplateFiled(Convert.ToInt16(pid[1]), Convert.ToInt16(ppid[1]), mode);
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}


