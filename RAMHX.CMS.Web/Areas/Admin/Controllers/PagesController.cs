using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Infra;
using RAMHX.CMS.Repository;
using RAMHX.CMS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Areas.Admin.Controllers
{
    public class PagesController : BaseController
    {
        public ActionResult RenderContent(Guid id)
        {
            RAMHX.CMS.Repository.PageRepository pgRepo = new Repository.PageRepository();
            var model = pgRepo.GetById(id, true);
            if (id != Guid.Empty)
            {

                if (SiteContext.CurrentUser != null && !string.IsNullOrEmpty(SiteContext.CurrentUser.Name))
                {
                    var pgPerm = pgRepo.HasPageAccessPermission(SiteContext.CurrentUser_Guid.ToString(), id);//.First();
                    if (!pgPerm)//.Key && !pgPerm.Value)
                    {
                        var acsdenConfig = (new AppConfiguration()).GetGroupItemByItemAndGroupID(Convert.ToInt32(Enums.AppConfigs.AccesDeniedGroupitem.ItemId), Convert.ToInt32(Enums.AppConfigs.AccesDeniedGroupitem.GroupId));
                        // redirect to access denied
                        if (acsdenConfig != null)
                        {
                            var accesDeniedPage = Repository.PageRepository.GetPageByUrl(acsdenConfig.ItemDesc);
                            if (accesDeniedPage != null)
                            {
                                return RedirectPermanent(accesDeniedPage.FullPageUrl);
                            }
                        }

                        return View("AccessDenied");
                    }
                }
                else
                {
                    // redirect to login
                    var pgPerm = pgRepo.HasPageAccessPermission(string.Empty, id);//.First();
                    if (!pgPerm)//.Key && !pgPerm.Value)
                    {
                        // redirect to custom login page
                        var loginpgConfig = (new AppConfiguration()).GetGroupItemByItemAndGroupID(Convert.ToInt32(Enums.AppConfigs.LoginPageItem.ItemId), Convert.ToInt32(Enums.AppConfigs.LoginPageItem.GroupId));
                        if (loginpgConfig != null)
                        {
                            var loginPage = Repository.PageRepository.GetPageByUrl(loginpgConfig.ItemDesc);
                            if (loginPage != null)
                            {
                                return RedirectPermanent(loginPage.FullPageUrl);
                            }
                        }
                        return RedirectToAction("Login", "Account", new { provider = model.Page.PageUrl });
                    }
                }
            }
            return View(model);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }

        // GET: Admin/Pages

        PageRepository pageRepo = new PageRepository();
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //var RootPages = pageRepo.GetPagesByParentPageID(Guid.Empty);
            List<PageHierarchy> Listpagehairarchy = new List<PageHierarchy>();
            //foreach (var item in RootPages)
            //{
            //    PageHierarchy pgh = new PageHierarchy();
            //    pgh.PageID = item.PageID.ToString();
            //    pgh.PageName = item.PageName;
            //    Listpagehairarchy.Add(pgh);
            //}
            return View(Listpagehairarchy);
        }

        /// <summary>
        /// Get root node
        /// </summary>
        /// <returns>Return Root node</returns>
        [Authorize(Roles = "Admin")]
        public JsonResult GetRootPage()
        {
            JsTreeItem rootNode = new JsTreeItem();
            // rootNode.attr = new JsTreeAttribute();
            rootNode.text = "Root";
            rootNode.id = Guid.Empty.ToString();// new DirectoryInfo(Request.MapPath("/")).Name;
            return Json(rootNode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Child nodes
        /// </summary>
        /// <param name="pageid">Parent Page id</param>
        /// <returns>Returns childerns</returns>
        [Authorize(Roles = "Admin")]
        public JsonResult GetPrimaryChildrens(string pageid)
        {
            Guid pageId = Guid.Parse(pageid);

            var RootPages = pageRepo.GetPagesByParentPageID(pageId);
            List<JsTreeItem> rootNode = new List<JsTreeItem>();
            foreach (var item in RootPages)
            {
                JsTreeItem nood = new JsTreeItem();
                nood.id = item.PageID.ToString();
                nood.text = item.PageName;
                nood.data = item.PageName;
                rootNode.Add(nood);
            }
            return Json(rootNode, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Method is used to delete selected page
        /// </summary>
        /// <param name="pageid">Page id</param>
        /// <returns>true if deleted or false if not</returns>
        [Authorize(Roles = "Admin")]
        public int DeleteNode(Guid pageid)
        {
            try
            {
                bool sucess = pageRepo.DeletePage(pageid);
                if (!sucess)
                    return 0;
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Method is used to delete child pages of selected page
        /// </summary>
        /// <param name="pageid">Page id</param>
        /// <returns>true if deleted or false if not</returns>
        [Authorize(Roles = "Admin")]
        public int DeleteChildren(Guid pageid)
        {
            try
            {
                pageRepo.DeleteChildren(pageid);
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// Move node
        /// </summary>
        /// <param name="pageid">page id</param>
        /// <param name="parentpageid">parent id</param>
        /// <returns>true if deleted or false if not</returns>
        [Authorize(Roles = "Admin")]
        public int MoveNode(Guid pageid, Guid parentpageid, Guid oldParent, string position)
        {
            try
            {
                pageRepo.MoveNode(pageid, parentpageid, oldParent, Convert.ToInt32(position));
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public JsonResult GetById(string id)
        {
            Guid gid = Infra.Common.GetGuidValue(id);
            PageRepository pr = new PageRepository();
            return Json(pr.GetById(gid), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult SaveData(PageModel pageModel)
        {
            PageRepository pr = new PageRepository();
            if (pageModel.Page.PageID != Guid.Empty)
                pageModel.Page.ModifiedDate = DateTime.Now;
            else
                pageModel.Page.CreatedDate = DateTime.Now;

            int OrderIndex = 1;

            List<cms_PageHTMLModules> module = new List<cms_PageHTMLModules>();
            if (pageModel.HtmlModules != null)
            {
                module = pageModel.HtmlModules.Select(hm => new cms_PageHTMLModules() { HTMLModuleId = hm.HTMLModuleId, CreatedDate = DateTime.Now, OrderIndex = OrderIndex++, PageID = pageModel.Page.PageID }).ToList();
            }

            pr.SavePageDetails(pageModel.Page);
            pr.UpdatePageHtmlModuleRelations(pageModel.Page.PageID, module);

            pr.RemoveRoles(pageModel.Page.PageID);
            if (pageModel.PageRoles != null)
            {
                foreach (var item in pageModel.PageRoles)
                {
                    pr.AddPageRoles(pageModel.Page.PageID, item.Id);
                }
            }

            pr.SyncTemplates(pageModel.PageTemplates, pageModel.Page.PageID);

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public JsonResult CutCopyPage(string pageid, string parentpageid, string mode)
        {
            Guid loggedInUser = new Guid();
            loggedInUser = Guid.NewGuid();

            Guid pid = Infra.Common.GetGuidValue(pageid);
            Guid ppid = Infra.Common.GetGuidValue(parentpageid);

            if (mode == "move_node")
            {
                pageRepo.CutClonePage(pid, ppid, loggedInUser);
            }
            if (mode == "copy_node")
            {
                pageRepo.CopyClonePage(pid, ppid, loggedInUser);
            }

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public JsonResult RenamePage(string pageid, string newPageName, string parentpageid, int? position)
        {
            cms_Pages newpage = new cms_Pages();
            Guid loggedInUser = new Guid();
            loggedInUser = Guid.NewGuid();

            Guid pid = Infra.Common.GetGuidValue(pageid);
            Guid ppid = Infra.Common.GetGuidValue(parentpageid);

            newpage.PageID = pid;
            if (newpage.PageID != Guid.Empty)
            {
                newpage = pageRepo.GetById(newpage.PageID).Page;
                newpage.PageName = newPageName;
            }
            else
            {
                newpage.PageUrl = newPageName;
                newpage.PageTitle = newPageName;
                newpage.PageOrder = position == null ? 0 : position.Value;
                newpage.PageName = newPageName;
                newpage.PageCode = newPageName;
            }

            newpage.ParentPageID = ppid;
            pageRepo.SavePageDetails(newpage);
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        #region PageFieldValues
        public ActionResult PageFieldValues(Guid pageid)
        {
            return View(pageRepo.GetPage(pageid));
        }

        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult SaveFieldData(FormCollection frm)
        {
            //type_PageID_TemplateId_TemplateFieldId_FieldTypeId
            //time_1_2_9_6
            List<cms_PageFieldValues> lstValues = new List<cms_PageFieldValues>();
            try
            {


                if (frm != null)
                {
                    foreach (var item in frm.AllKeys)
                    {
                        var idSplited = item.Split('_');
                        if (idSplited.Count() > 4)
                        {
                            Guid pageID = Infra.Common.GetGuidValue(idSplited[1]);
                            Guid TemplateId = Infra.Common.GetGuidValue(idSplited[2]);
                            Guid TemplateFieldId = Infra.Common.GetGuidValue(idSplited[3]);
                            int FieldTtypeID = Infra.Common.GetIntValue(idSplited[4]);
                            if (pageID != Guid.Empty && TemplateId != Guid.Empty && TemplateFieldId != Guid.Empty && !string.IsNullOrEmpty(frm[item]))
                            {
                                cms_PageFieldValues pgFv = new cms_PageFieldValues() { PageId = pageID, TemplateFieldId = TemplateFieldId, FieldValue = GetFieldValue(frm[item], (Enums.FieldTypes)FieldTtypeID), TemplateId = TemplateId };
                                lstValues.Add(pgFv);
                            }
                        }
                    }
                    pageRepo.UpdateFieldValues(lstValues);
                }
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.logger.Error("Error Occured PagesController>SaveFieldData-" + ex.Message, ex);
                return Json(new { status = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }

        private string GetFieldValue(string pgFv, Enums.FieldTypes type)
        {
            switch (type)
            {
                case Enums.FieldTypes.Date:
                    var date = pgFv.Split('/');
                    DateTime dt = new DateTime(Infra.Common.GetIntValue(date[2]), Infra.Common.GetIntValue(date[0]), Infra.Common.GetIntValue(date[1]));
                    return dt.ToString("yyyyMMdd");
                case Enums.FieldTypes.Time:
                    var time = pgFv.Split(':');
                    int hour = Infra.Common.GetIntValue(time[0].Trim());
                    if (time[2].Trim().ToLower() == "pm")
                    {
                        hour = hour + 12;
                    }
                    TimeSpan ts = new TimeSpan(hour, Infra.Common.GetIntValue(time[1].Trim()), 0);
                    DateTime tm = DateTime.Today.Add(ts);
                    return tm.ToString("hh:mm tt");
                    break;
            }
            return pgFv;
        }
        #endregion

        [Authorize(Roles = "Admin")]
        public JsonResult SaveTemplateAndField(string pageid, string tmpid)
        {
            TemplatesRepository tempsRepo = new TemplatesRepository();
            Guid pid = Infra.Common.GetGuidValue(pageid);
            Guid tid = Infra.Common.GetGuidValue(tmpid);

            foreach (var item in tmpid.Split(','))
            {
                pageRepo.AssignTemplate(pid, tid);
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}