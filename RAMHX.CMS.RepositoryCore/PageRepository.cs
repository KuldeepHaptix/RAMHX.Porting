using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using RAMHX.CMS.DataAccessCore;
using RAMHX.CMS.InfraCore;


namespace RAMHX.CMS.RepositoryCore
{
    public class PageRepository : BaseRepository
    {
        public PageRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
        public static Guid MainRootPageId = Guid.Parse("628CE6A0-BEF9-4505-89B9-A49F76646318");

        public PageModel GetById(Guid pageId, bool loadModuleWithContent = true)
        {
            PageModel model = new PageModel();
            var cPage = dataContext.CmsPages.FirstOrDefault(page => page.PageId == pageId);
            if (cPage != null)
            {
                model.Page = new DataAccessCore.CmsPages() { PageId = cPage.PageId, PageTitle = cPage.PageTitle, PageName = cPage.PageName, Description = cPage.Description, FullPageUrl = cPage.FullPageUrl, PageCode = cPage.PageCode, ParentPageId = cPage.ParentPageId, PageUrl = cPage.PageUrl, PageOrder = cPage.PageOrder, PageMetaKeywords = cPage.PageMetaKeywords, PageLayoutPath = cPage.PageLayoutPath, PageMetaDescription = cPage.PageMetaDescription, CreatedByUserId = cPage.CreatedByUserId, CreatedDate = cPage.CreatedDate, ModifiedByUserId = cPage.ModifiedByUserId, ModifiedDate = cPage.ModifiedDate, FullItemPath = cPage.FullItemPath, ShowInNavigation = cPage.ShowInNavigation };
                List<CmsPageFieldValues> lstPageFieldVals = new List<CmsPageFieldValues>();
                foreach (var pgField in dataContext.CmsPageFieldValues.Where(p => p.PageId == pageId).ToList())
                {
                    lstPageFieldVals.Add(new CmsPageFieldValues()
                    {
                        PageId = pgField.PageId,
                        FieldValue = pgField.FieldValue,
                        TemplateFieldId = pgField.TemplateFieldId,
                        TemplateId = pgField.TemplateId
                    });
                }
                model.Page.CmsPageFieldValues = lstPageFieldVals;
                List<CmsTemplates> lstPageTemplates = new List<CmsTemplates>();

                foreach (var tp in dataContext.CmsTemplates.Where(tmp => tmp.CmsPages.FirstOrDefault(t => t.PageId == pageId) != null).ToList())
                {
                    lstPageTemplates.Add(new CmsTemplates
                    {
                        TemplateName = tp.TemplateName,
                        CreatedByUserId = tp.CreatedByUserId,
                        CreatedDate = tp.CreatedDate,
                        Description = tp.Description,
                        ModifiedByUserId = tp.ModifiedByUserId,
                        ModifiedDate = tp.ModifiedDate,
                        TemplateCode = tp.TemplateCode,
                        TemplateId = tp.TemplateId
                    });
                }
                model.Page.CmsTemplates = lstPageTemplates;
                model.Page.FullPageUrl = SiteContext.Pages.First(p => p.PageId == pageId).FullPageUrl;
                model.Page.FullItemPath = SiteContext.Pages.First(p => p.PageId == pageId).FullItemPath;
                model.HtmlModules = GetHtmlModules(pageId, loadModuleWithContent);
                model.PageRoles = GetPageRoleTextByID(pageId);
            }
            return model;
        }
        public List<CmsPages> GetAllPages()
        {
            return dataContext.CmsPages.ToList();
        }

        public CmsPages GetPage(Guid pageid)
        {
            return dataContext.CmsPages.FirstOrDefault(c => c.PageId == pageid);
        }

        public void AssignTemplate(Guid pageid, Guid templateId)
        {
            var cmsPage = this.dataContext.CmsPages.FirstOrDefault(page => page.PageId == pageid);
            var cmsTmpl = this.dataContext.CmsTemplates.FirstOrDefault(tmpl => tmpl.TemplateId == templateId);
            if (cmsPage != null && cmsTmpl != null)
            {
                cmsPage.CmsTemplates.Add(cmsTmpl);
                this.dataContext.SaveChanges();
            }
        }

        public void RemoveTemplate(Guid pageid, Guid templateId)
        {
            var cmsPage = this.dataContext.CmsPages.FirstOrDefault(page => page.PageId == pageid);
            var cmsTmpl = this.dataContext.CmsTemplates.FirstOrDefault(tmpl => tmpl.TemplateId == templateId);
            if (cmsPage != null && cmsTmpl != null)
            {
                cmsPage.CmsTemplates.Remove(cmsTmpl);
                this.dataContext.SaveChanges();
            }
        }

        public void UpdatePageModel(PageModel pageModel)
        {
            if (pageModel.Page.PageId != Guid.Empty)
                pageModel.Page.ModifiedDate = DateTime.Now;
            else
                pageModel.Page.CreatedDate = DateTime.Now;

            int OrderIndex = 1;

            List<CmsPageHtmlmodules> module = new List<CmsPageHtmlmodules>();
            if (pageModel.HtmlModules != null)
            {
                module = pageModel.HtmlModules.Select(hm => new CmsPageHtmlmodules() { HtmlmoduleId = hm.HtmlmoduleId, CreatedDate = DateTime.Now, OrderIndex = OrderIndex++, PageId = pageModel.Page.PageId }).ToList();
            }

            SavePageDetails(pageModel.Page);
            UpdatePageHtmlModuleRelations(pageModel.Page.PageId, module);

            RemoveRoles(pageModel.Page.PageId);
            if (pageModel.PageRoles != null)
            {
                foreach (var item in pageModel.PageRoles)
                {
                    AddPageRoles(pageModel.Page.PageId, item.Id);
                }
            }


            if (pageModel.PageTemplates != null)
            {
                var dbpageTmpls = GetPage(pageModel.Page.PageId).CmsTemplates.ToList();

                foreach (var item in dbpageTmpls)
                {
                    if (pageModel.PageTemplates.FirstOrDefault(pt => pt.TemplateId == item.TemplateId) == null)
                        RemoveTemplate(pageModel.Page.PageId, item.TemplateId);
                }
            }
        }

        public void UpdateFieldValues(List<CmsPageFieldValues> values)
        {
            foreach (var value in values)
            {
                var currValue = this.dataContext.CmsPageFieldValues.FirstOrDefault(pfv => pfv.PageId == value.PageId && pfv.TemplateId == value.TemplateId && pfv.TemplateFieldId == value.TemplateFieldId);
                if (currValue != null)
                {
                    currValue.FieldValue = value.FieldValue;
                }
                else
                {
                    this.dataContext.CmsPageFieldValues.Add(value);
                }
            }

            if (values.Count > 0)
            {
                this.dataContext.SaveChanges();
                RefreshPages();
            }
        }

        public List<HtmlModule> GetHtmlModules(Guid pageId, bool loadContent)
        {
            var modules = (from hm in dataContext.HtmlModule
                           join pagehm in dataContext.CmsPageHtmlmodules on hm.HtmlmoduleId equals pagehm.HtmlmoduleId
                           orderby pagehm.OrderIndex
                           where pagehm.PageId == pageId
                           select hm).ToList();

            //Set HTML by code or prefix - recursive method
            //RenderHtmlRepository htmlRepo = new RenderHtmlRepository();
            //foreach (var mod in modules)
            //{
            //    string html = htmlRepo.GenerateHtml(mod.HtmlModuleHTML);
            //}

            if (!loadContent)
                modules.ForEach(m => m.HtmlModuleHtml = "");

            return modules;
        }

        public List<CmsPageRoles> GetPageRolesByID(Guid pageid)
        {
            return dataContext.CmsPageRoles.Where(pid => pid.PageId == pageid).ToList();
        }

        public List<AspNetRoles> GetPageRoleTextByID(Guid pageid)
        {
            var assignedRoles = GetPageRolesByID(pageid);
            List<AspNetRoles> assignedRolesDetail = new List<AspNetRoles>();
            foreach (var item in assignedRoles)
            {
                var ridstr = item.RoleId.ToString();
                AspNetRoles role = new AspNetRoles();
                var roledetail = dataContext.AspNetRoles.Where(rid => rid.Id == ridstr).FirstOrDefault();
                role.Id = ridstr;
                role.Name = roledetail.Name;
                assignedRolesDetail.Add(role);
            }
            return assignedRolesDetail;
        }

        public void SyncTemplates(List<CmsTemplates> pageTemplates, Guid PageID)
        {
            var page = dataContext.CmsPages.First(p => p.PageId == PageID);
            var pts = page.CmsTemplates.ToList();
            foreach (var item in pts)
            {
                page.CmsTemplates.Remove(item);
                dataContext.SaveChanges();
            }
            if (pageTemplates != null && pageTemplates.Count > 0)
            {
                foreach (var item in pageTemplates)
                {
                    AssignTemplate(PageID, item.TemplateId);
                }
            }

        }

        public bool AddPageHtmlModule(Guid pageId, Guid htmlModuleId)
        {
            try
            {
                int order = 1;
                var modules = dataContext.CmsPageHtmlmodules.Where(page => page.PageId == pageId).ToList();
                if (modules.Count > 0)
                    order = modules.Select(m => m.OrderIndex).Max() + 1;

                CmsPageHtmlmodules pageHm = new CmsPageHtmlmodules()
                {
                    CreatedDate = DateTime.Now,
                    HtmlmoduleId = htmlModuleId,
                    PageId = pageId,
                    OrderIndex = order,
                    PageHtmlmoduleId = Guid.NewGuid()
                };

                dataContext.CmsPageHtmlmodules.Add(pageHm);
                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }

            return true;
        }

        public bool AddPageRoles(Guid pageId, string RoleId)
        {
            try
            {
                CmsPageRoles pageHm = new CmsPageRoles()
                {
                    PageId = pageId,
                    RoleId = RoleId
                };

                dataContext.CmsPageRoles.Add(pageHm);
                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }

            return true;
        }

        public bool RemoveRoles(Guid pageId)
        {
            var modules = dataContext.CmsPageRoles.Where(page => page.PageId == pageId).ToList();
            if (modules.Count > 0)
            {
                dataContext.CmsPageRoles.RemoveRange(modules);
                dataContext.SaveChanges();
                return true;
            }
            return false;
        }

        public AspNetRoles GetRoleByRoleID(string roleid)
        {
            return dataContext.AspNetRoles.Where(rid => rid.Id == roleid).FirstOrDefault();
        }

        public bool UpdatePageHtmlModuleRelations(Guid pageId, List<CmsPageHtmlmodules> moduleIds)
        {
            try
            {
                var currentMods = dataContext.CmsPageHtmlmodules.Where(page => page.PageId == pageId);
                dataContext.CmsPageHtmlmodules.RemoveRange(currentMods);

                dataContext.CmsPageHtmlmodules.AddRange((from m in moduleIds
                                                          select new CmsPageHtmlmodules() { PageId = pageId, CreatedByUserId = m.CreatedByUserId, CreatedDate = DateTime.Now, HtmlmoduleId = m.HtmlmoduleId, OrderIndex = m.OrderIndex, PageHtmlmoduleId = Guid.NewGuid() }).ToList());

                dataContext.SaveChanges();

                RefreshPages();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }

            return true;
        }

        public bool SavePageDetails(CmsPages page)
        {
            CmsPages dbPage = new CmsPages();
            try
            {
                if (page.PageId != Guid.Empty)
                    dbPage = dataContext.CmsPages.First(p => p.PageId == page.PageId);
                else
                {
                    dbPage.PageId = Guid.NewGuid();
                }

                dbPage.PageUrl = page.PageUrl;
                dbPage.PageTitle = page.PageTitle;
                dbPage.PageOrder = page.PageOrder;
                dbPage.PageName = page.PageName;
                dbPage.PageMetaKeywords = page.PageMetaKeywords;
                dbPage.PageMetaDescription = page.PageMetaDescription;
                dbPage.PageLayoutPath = page.PageLayoutPath;
                dbPage.PageCode = page.PageCode;
                dbPage.Description = page.Description;
                dbPage.ShowInNavigation = page.ShowInNavigation;

                if (page.PageId != Guid.Empty)
                {
                    dbPage.ModifiedByUserId = page.ModifiedByUserId;
                    dbPage.ModifiedDate = page.ModifiedDate;
                }
                else
                {
                    dbPage.CreatedDate = DateTime.Now;
                    dbPage.CreatedByUserId = page.CreatedByUserId;
                    dbPage.ParentPageId = page.ParentPageId;
                }

                if (page.PageId == Guid.Empty)
                    dataContext.CmsPages.Add(dbPage);

                dataContext.SaveChanges();
                RefreshPages();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Cut clone page and paste
        /// </summary>
        /// <param name="pageid">page id</param>
        /// <param name="ParentID">parent page id</param>
        public void CutClonePage(Guid pageid, Guid ParentID, Guid currentUserId)
        {
            var pages = dataContext.CmsPages.Where(id => id.PageId == pageid).FirstOrDefault();
            pages.ParentPageId = ParentID;
            pages.ModifiedByUserId = currentUserId;
            pages.ModifiedDate = DateTime.Now;
            dataContext.SaveChanges();
            RefreshPages();
        }

        public bool CopyClonePage(Guid pageid, Guid parentPageId, Guid currentUserId)
        {
            CmsPages dbPage = new CmsPages();
            try
            {
                var page = dataContext.CmsPages.First(p => p.PageId == pageid);
                dbPage.PageCode = page.PageCode + "_copy";
                dbPage.PageUrl = page.PageUrl + "_copy";
                dbPage.PageTitle = page.PageTitle + "_copy";
                dbPage.PageOrder = page.PageOrder;
                dbPage.PageName = page.PageName + "_copy";
                dbPage.PageMetaKeywords = page.PageMetaKeywords;
                dbPage.PageMetaDescription = page.PageMetaDescription;
                dbPage.PageLayoutPath = page.PageLayoutPath;
                dbPage.Description = page.Description;
                dbPage.CreatedDate = DateTime.Now;
                dbPage.CreatedByUserId = currentUserId;
                dbPage.ParentPageId = parentPageId;
                dbPage.PageId = Guid.NewGuid();

                dataContext.CmsPages.Add(dbPage);

                dataContext.SaveChanges();

                foreach (var item in dataContext.CmsPageRoles.Where(pr => pr.PageId == pageid).ToList())
                {
                    AddPageRoles(dbPage.PageId, item.RoleId);
                }

                foreach (var item in page.CmsTemplates)
                {
                    AssignTemplate(dbPage.PageId, item.TemplateId);
                }

                foreach (var item in dataContext.CmsPageHtmlmodules.Where(pr => pr.PageId == pageid).ToList())
                {
                    AddPageHtmlModule(dbPage.PageId, item.HtmlmoduleId.Value);
                }

                RefreshPages();

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Return all child pages
        /// </summary>
        /// <returns>Get Root pages</returns>
        public List<CmsPages> GetPagesByParentPageID(Guid parentPageID)
        {
            List<CmsPages> pageList = new List<CmsPages>();
            try
            {
                var allPages = dataContext.CmsPages.ToList().OrderBy(id => id.PageOrder);
                foreach (var item in allPages)
                {
                    if (item.ParentPageId == parentPageID || (parentPageID == Guid.Empty && item.ParentPageId == null))
                    {
                        CmsPages page = new CmsPages();
                        page.PageId = item.PageId;
                        page.PageName = item.PageName;
                        page.PageOrder = item.PageOrder;
                        page.PageCode = item.PageCode;
                        page.ParentPageId = item.ParentPageId;
                        page.FullPageUrl = SiteContext.Pages.First(p => p.PageId == item.PageId).FullPageUrl;
                        page.FullItemPath =SiteContext.Pages.First(p => p.PageId == item.PageId).FullItemPath;
                        pageList.Add(page);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
            return pageList;
        }

        /// <summary>
        /// Dlete Page only
        /// </summary>
        /// <param name="pageID">Page id</param>
        /// <returns>true/false</returns>
        public bool DeletePage(Guid pageID)
        {
            var childrens = dataContext.CmsPages.Where(id => id.ParentPageId == pageID).Count();
            if (childrens > 0)
            {
                return false;
            }
            try
            {
                var page = dataContext.CmsPages.Where(id => id.PageId == pageID).FirstOrDefault();
                dataContext.CmsPageHtmlmodules.RemoveRange(dataContext.CmsPageHtmlmodules.Where(pm => pm.PageId == pageID));
                dataContext.CmsPageFieldValues.RemoveRange(dataContext.CmsPageFieldValues.Where(pfv => pfv.PageId == pageID));

                var pts = page.CmsTemplates.ToList();
                foreach (var item in pts)
                {
                    page.CmsTemplates.Remove(item);
                }

                dataContext.CmsPages.Remove(page);
                dataContext.SaveChanges();
                RefreshPages();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }

            return true;
        }

        /// <summary>
        /// Delete childrens 
        /// </summary>
        /// <param name="pageID">Page id</param>
        /// <returns>true/false</returns>
        public void DeleteChildren(Guid pageID)
        {
            var page = dataContext.CmsPages.Where(id => id.ParentPageId == pageID).ToList();
            dataContext.CmsPages.RemoveRange(page);
            dataContext.SaveChanges();

            DeletePage(pageID);
            RefreshPages();
        }

        /// <summary>
        /// Move node
        /// </summary>
        /// <param name="pageid">page id</param>
        /// <param name="ParentID">parent page id</param>
        public void MoveNode(Guid pageid, Guid ParentID, Guid oldparent, int position)
        {
            var pages = dataContext.CmsPages.Where(id => id.PageId == pageid).FirstOrDefault();
            pages.ParentPageId = ParentID;
            if (pages.ParentPageId == oldparent)
            {
                pages.ParentPageId = oldparent;
                pages.PageOrder = position - 1;
            }
            dataContext.SaveChanges();

            RefreshPages();
        }

        public  List<CmsPages> RefreshPages()
        {
            SiteContext.Pages = new List<CmsPages>();
            PageRepository pageRepo = new PageRepository(this.Context);
            var page = pageRepo.GetPage(MainRootPageId);
            page.FullPageUrl = "/";
            page.FullItemPath = "/";
            SiteContext.Pages.Add(page);
            foreach (var subpage in page.InverseParentPage)
            {
                AddPageToCache(subpage);
            }
            return SiteContext.Pages;
        }

        private static void AddPageToCache(CmsPages page)
        {
            page.FullPageUrl = "/" + page.PageUrl;
            page.FullItemPath = "/" + GetParsedPath(page.PageName.ToLower());

            if (page.ParentPageId != Guid.Empty && page.ParentPageId != null)
            {
                var pp = SiteContext.Pages.FirstOrDefault(p => p.PageId == page.ParentPageId);
                if (pp != null)
                {
                    var purl = page.PageUrl;
                    if (string.IsNullOrEmpty(purl))
                    {
                        purl = page.PageTitle;
                    }
                    if (string.IsNullOrEmpty(purl))
                    {
                        purl = page.PageName;
                    }
                    if (string.IsNullOrEmpty(purl))
                    {
                        purl = page.PageCode;
                    }
                    page.FullPageUrl = pp.FullPageUrl + "/" + purl.ToLower();// page.PageUrl;
                    page.FullItemPath = GetParsedPath(pp.FullItemPath + "/" + page.PageName.ToLower());
                }
            }

            page.FullPageUrl = page.FullPageUrl.ToLower().Replace("//", "/").Replace("/hidden/", "/");
            page.FullItemPath = GetParsedPath(page.FullItemPath.ToLower().Replace("//", "/"));

            SiteContext.Pages.Add(page);

            foreach (var subpage in page.InverseParentPage)
            {
                AddPageToCache(subpage);
            }
        }

        public static string GetParsedPath(string path)
        {
            string pattern = "[^a-zA-Z0-9_/]";
            string replacement = "-";

            Regex regEx = new Regex(pattern);
            string sanitized = Regex.Replace(regEx.Replace(path, replacement), @"\s+", "-");

            return sanitized;
        }
        
        public bool HasPageAccessPermission(string id, Guid pageid)
        {
            SiteContext st = new SiteContext(this.Context);
            var pageRoles = dataContext.CmsPageRoles.Where(pg => pg.PageId == pageid && pg.RoleId != "").ToList();
            if (pageRoles.Count == 0 || st.CurrentUser_IsAdmin)
            {
                return true;
            }

            if (!string.IsNullOrEmpty(id))
            {
                //var userRoles = dataContext.AspNetUsers.FirstOrDefault(u => u.Id == id).AspNetRoles.ToList();
                var userRoles = st.CurrentUser_Roles;
                foreach (var pgrole in pageRoles)
                {
                    if (userRoles.Count(ur => ur.Id == pgrole.RoleId) > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static CmsPages GetPageByUrl(string requestPath)
        {
            requestPath = Path.Combine(requestPath).Split('?').GetValue(0).ToString().ToLower();
            foreach (var page in SiteContext.Pages)
            {
                if (page.FullPageUrl.ToLower() == requestPath)
                {
                    return page;
                }
            }
            return null;
        }

        public static string PageNotFound()
        {
            var pagenotfoundConf = (new AppConfiguration()).GetGroupItemByItemAndGroupID(Convert.ToInt32(InfraCore.Enums.AppConfigs.PageNotFoundItem.ItemId), Convert.ToInt32(InfraCore.Enums.AppConfigs.PageNotFoundItem.GroupId));
            if (pagenotfoundConf != null)
            {
                var pageNotFound = RepositoryCore.PageRepository.GetPageByUrl(pagenotfoundConf.ItemDesc);
                if (pageNotFound != null)
                {
                    return pageNotFound.FullPageUrl;
                }
            }
            return "/Admin/Pages/PageNotFound";
        }

        public void SaveTemplateData(FormCollection frm, string ids)
        {
            var idSplited = ids.Split('_');
            Guid pageID = InfraCore.Common.GetGuidValue(idSplited[1]);
            Guid TemplateId = InfraCore.Common.GetGuidValue(idSplited[2]);
            Guid TemplateFieldId = InfraCore.Common.GetGuidValue(idSplited[3]);
            int FieldTtypeID = InfraCore.Common.GetIntValue(idSplited[4]);
            if (pageID != Guid.Empty && TemplateId != Guid.Empty && TemplateFieldId != Guid.Empty)
            {
                var fieldRecord = dataContext.CmsPageFieldValues.FirstOrDefault(fl => fl.PageId == pageID && fl.TemplateId == TemplateId && fl.TemplateFieldId == TemplateFieldId);
                if (fieldRecord != null)
                {
                    fieldRecord.FieldValue = frm[ids];
                    dataContext.SaveChanges();
                }
                else
                {
                    CmsPageFieldValues pageFlVal = new CmsPageFieldValues();
                    pageFlVal.PageId = pageID;
                    pageFlVal.TemplateId = TemplateId;
                    pageFlVal.TemplateFieldId = TemplateFieldId;
                    pageFlVal.FieldValue = frm[ids];
                    dataContext.CmsPageFieldValues.Add(pageFlVal);
                    dataContext.SaveChanges();
                }
            }
        }

        public HtmlModule GetHtmlModule(string code)
        {
            return (from hm in dataContext.HtmlModule
                    where hm.HtmlModuleCode == code
                    select hm).FirstOrDefault();
        }
    }
}
