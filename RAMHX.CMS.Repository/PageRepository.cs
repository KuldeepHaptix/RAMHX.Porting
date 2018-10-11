using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace RAMHX.CMS.Repository
{
    public class PageRepository : BaseRepository
    {
        public static Guid MainRootPageId = Guid.Parse("628CE6A0-BEF9-4505-89B9-A49F76646318");

        public PageModel GetById(Guid pageId, bool loadModuleWithContent = true)
        {
            PageModel model = new PageModel();
            var cPage = dataContext.cms_Pages.FirstOrDefault(page => page.PageID == pageId);
            if (cPage != null)
            {
                model.Page = new cms_Pages() { PageID = cPage.PageID, PageTitle = cPage.PageTitle, PageName = cPage.PageName, Description = cPage.Description, FullPageUrl = cPage.FullPageUrl, PageCode = cPage.PageCode, ParentPageID = cPage.ParentPageID, PageUrl = cPage.PageUrl, PageOrder = cPage.PageOrder, PageMetaKeywords = cPage.PageMetaKeywords, PageLayoutPath = cPage.PageLayoutPath, PageMetaDescription = cPage.PageMetaDescription, CreatedByUserId = cPage.CreatedByUserId, CreatedDate = cPage.CreatedDate, ModifiedByUserId = cPage.ModifiedByUserId, ModifiedDate = cPage.ModifiedDate, FullItemPath = cPage.FullItemPath, ShowInNavigation = cPage.ShowInNavigation };
                List<cms_PageFieldValues> lstPageFieldVals = new List<cms_PageFieldValues>();
                foreach (var pgField in dataContext.cms_PageFieldValues.Where(p => p.PageId == pageId).ToList())
                {
                    lstPageFieldVals.Add(new cms_PageFieldValues()
                    {
                        PageId = pgField.PageId,
                        FieldValue = pgField.FieldValue,
                        TemplateFieldId = pgField.TemplateFieldId,
                        TemplateId = pgField.TemplateId
                    });
                }
                model.Page.cms_PageFieldValues = lstPageFieldVals;
                List<cms_Templates> lstPageTemplates = new List<cms_Templates>();

                foreach (var tp in dataContext.cms_Templates.Where(tmp => tmp.cms_Pages.FirstOrDefault(t => t.PageID == pageId) != null).ToList())
                {
                    lstPageTemplates.Add(new cms_Templates
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
                model.Page.cms_Templates = lstPageTemplates;
                model.Page.FullPageUrl = RAMHX.CMS.Web.SiteContext.Pages.First(p => p.PageID == pageId).FullPageUrl;
                model.Page.FullItemPath = RAMHX.CMS.Web.SiteContext.Pages.First(p => p.PageID == pageId).FullItemPath;
                model.HtmlModules = GetHtmlModules(pageId, loadModuleWithContent);
                model.PageRoles = GetPageRoleTextByID(pageId);
            }
            return model;
        }
        public List<cms_Pages> GetAllPages()
        {
            return dataContext.cms_Pages.ToList();
        }

        public cms_Pages GetPage(Guid pageid)
        {
            return dataContext.cms_Pages.FirstOrDefault(c => c.PageID == pageid);
        }

        public void AssignTemplate(Guid pageid, Guid templateId)
        {
            var cmsPage = this.dataContext.cms_Pages.FirstOrDefault(page => page.PageID == pageid);
            var cmsTmpl = this.dataContext.cms_Templates.FirstOrDefault(tmpl => tmpl.TemplateId == templateId);
            if (cmsPage != null && cmsTmpl != null)
            {
                cmsPage.cms_Templates.Add(cmsTmpl);
                this.dataContext.SaveChanges();
            }
        }

        public void RemoveTemplate(Guid pageid, Guid templateId)
        {
            var cmsPage = this.dataContext.cms_Pages.FirstOrDefault(page => page.PageID == pageid);
            var cmsTmpl = this.dataContext.cms_Templates.FirstOrDefault(tmpl => tmpl.TemplateId == templateId);
            if (cmsPage != null && cmsTmpl != null)
            {
                cmsPage.cms_Templates.Remove(cmsTmpl);
                this.dataContext.SaveChanges();
            }
        }

        public void UpdatePageModel(PageModel pageModel)
        {
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

            SavePageDetails(pageModel.Page);
            UpdatePageHtmlModuleRelations(pageModel.Page.PageID, module);

            RemoveRoles(pageModel.Page.PageID);
            if (pageModel.PageRoles != null)
            {
                foreach (var item in pageModel.PageRoles)
                {
                    AddPageRoles(pageModel.Page.PageID, item.Id);
                }
            }


            if (pageModel.PageTemplates != null)
            {
                var dbpageTmpls = GetPage(pageModel.Page.PageID).cms_Templates.ToList();

                foreach (var item in dbpageTmpls)
                {
                    if (pageModel.PageTemplates.FirstOrDefault(pt => pt.TemplateId == item.TemplateId) == null)
                        RemoveTemplate(pageModel.Page.PageID, item.TemplateId);
                }
            }
        }

        public void UpdateFieldValues(List<cms_PageFieldValues> values)
        {
            foreach (var value in values)
            {
                var currValue = this.dataContext.cms_PageFieldValues.FirstOrDefault(pfv => pfv.PageId == value.PageId && pfv.TemplateId == value.TemplateId && pfv.TemplateFieldId == value.TemplateFieldId);
                if (currValue != null)
                {
                    currValue.FieldValue = value.FieldValue;
                }
                else
                {
                    this.dataContext.cms_PageFieldValues.Add(value);
                }
            }

            if (values.Count > 0)
            {
                this.dataContext.SaveChanges();
                PageRepository.RefreshPages();
            }
        }

        public List<HtmlModule> GetHtmlModules(Guid pageId, bool loadContent)
        {
            var modules = (from hm in dataContext.HtmlModules
                           join pagehm in dataContext.cms_PageHTMLModules on hm.HTMLModuleId equals pagehm.HTMLModuleId
                           orderby pagehm.OrderIndex
                           where pagehm.PageID == pageId
                           select hm).ToList();

            //Set HTML by code or prefix - recursive method
            //RenderHtmlRepository htmlRepo = new RenderHtmlRepository();
            //foreach (var mod in modules)
            //{
            //    string html = htmlRepo.GenerateHtml(mod.HtmlModuleHTML);
            //}

            if (!loadContent)
                modules.ForEach(m => m.HtmlModuleHTML = "");

            return modules;
        }

        public List<cms_PageRoles> GetPageRolesByID(Guid pageid)
        {
            return dataContext.cms_PageRoles.Where(pid => pid.PageId == pageid).ToList();
        }

        public List<AspNetRole> GetPageRoleTextByID(Guid pageid)
        {
            var assignedRoles = GetPageRolesByID(pageid);
            List<AspNetRole> assignedRolesDetail = new List<AspNetRole>();
            foreach (var item in assignedRoles)
            {
                var ridstr = item.RoleId.ToString();
                AspNetRole role = new AspNetRole();
                var roledetail = dataContext.AspNetRoles.Where(rid => rid.Id == ridstr).FirstOrDefault();
                role.Id = ridstr;
                role.Name = roledetail.Name;
                assignedRolesDetail.Add(role);
            }
            return assignedRolesDetail;
        }

        public void SyncTemplates(List<cms_Templates> pageTemplates, Guid PageID)
        {
            var page = dataContext.cms_Pages.First(p => p.PageID == PageID);
            var pts = page.cms_Templates.ToList();
            foreach (var item in pts)
            {
                page.cms_Templates.Remove(item);
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
                var modules = dataContext.cms_PageHTMLModules.Where(page => page.PageID == pageId).ToList();
                if (modules.Count > 0)
                    order = modules.Select(m => m.OrderIndex).Max() + 1;

                cms_PageHTMLModules pageHm = new cms_PageHTMLModules()
                {
                    CreatedDate = DateTime.Now,
                    HTMLModuleId = htmlModuleId,
                    PageID = pageId,
                    OrderIndex = order,
                    PageHTMLModuleId = Guid.NewGuid()
                };

                dataContext.cms_PageHTMLModules.Add(pageHm);
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
                cms_PageRoles pageHm = new cms_PageRoles()
                {
                    PageId = pageId,
                    RoleId = RoleId
                };

                dataContext.cms_PageRoles.Add(pageHm);
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
            var modules = dataContext.cms_PageRoles.Where(page => page.PageId == pageId).ToList();
            if (modules.Count > 0)
            {
                dataContext.cms_PageRoles.RemoveRange(modules);
                dataContext.SaveChanges();
                return true;
            }
            return false;
        }

        public AspNetRole GetRoleByRoleID(string roleid)
        {
            return dataContext.AspNetRoles.Where(rid => rid.Id == roleid).FirstOrDefault();
        }

        public bool UpdatePageHtmlModuleRelations(Guid pageId, List<cms_PageHTMLModules> moduleIds)
        {
            try
            {
                var currentMods = dataContext.cms_PageHTMLModules.Where(page => page.PageID == pageId);
                dataContext.cms_PageHTMLModules.RemoveRange(currentMods);

                dataContext.cms_PageHTMLModules.AddRange((from m in moduleIds
                                                          select new cms_PageHTMLModules() { PageID = pageId, CreatedByUserId = m.CreatedByUserId, CreatedDate = DateTime.Now, HTMLModuleId = m.HTMLModuleId, OrderIndex = m.OrderIndex, PageHTMLModuleId = Guid.NewGuid() }).ToList());

                dataContext.SaveChanges();

                PageRepository.RefreshPages();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }

            return true;
        }

        public bool SavePageDetails(cms_Pages page)
        {
            cms_Pages dbPage = new cms_Pages();
            try
            {
                if (page.PageID != Guid.Empty)
                    dbPage = dataContext.cms_Pages.First(p => p.PageID == page.PageID);
                else
                {
                    dbPage.PageID = Guid.NewGuid();
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

                if (page.PageID != Guid.Empty)
                {
                    dbPage.ModifiedByUserId = page.ModifiedByUserId;
                    dbPage.ModifiedDate = page.ModifiedDate;
                }
                else
                {
                    dbPage.CreatedDate = DateTime.Now;
                    dbPage.CreatedByUserId = page.CreatedByUserId;
                    dbPage.ParentPageID = page.ParentPageID;
                }

                if (page.PageID == Guid.Empty)
                    dataContext.cms_Pages.Add(dbPage);

                dataContext.SaveChanges();
                PageRepository.RefreshPages();
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
            var pages = dataContext.cms_Pages.Where(id => id.PageID == pageid).FirstOrDefault();
            pages.ParentPageID = ParentID;
            pages.ModifiedByUserId = currentUserId;
            pages.ModifiedDate = DateTime.Now;
            dataContext.SaveChanges();
            PageRepository.RefreshPages();
        }

        public bool CopyClonePage(Guid pageid, Guid parentPageId, Guid currentUserId)
        {
            cms_Pages dbPage = new cms_Pages();
            try
            {
                var page = dataContext.cms_Pages.First(p => p.PageID == pageid);
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
                dbPage.ParentPageID = parentPageId;
                dbPage.PageID = Guid.NewGuid();

                dataContext.cms_Pages.Add(dbPage);

                dataContext.SaveChanges();

                foreach (var item in dataContext.cms_PageRoles.Where(pr => pr.PageId == pageid).ToList())
                {
                    AddPageRoles(dbPage.PageID, item.RoleId);
                }

                foreach (var item in page.cms_Templates)
                {
                    AssignTemplate(dbPage.PageID, item.TemplateId);
                }

                foreach (var item in dataContext.cms_PageHTMLModules.Where(pr => pr.PageID == pageid).ToList())
                {
                    AddPageHtmlModule(dbPage.PageID, item.HTMLModuleId.Value);
                }

                PageRepository.RefreshPages();

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
        public List<cms_Pages> GetPagesByParentPageID(Guid parentPageID)
        {
            List<cms_Pages> pageList = new List<cms_Pages>();
            try
            {
                var allPages = dataContext.cms_Pages.ToList().OrderBy(id => id.PageOrder);
                foreach (var item in allPages)
                {
                    if (item.ParentPageID == parentPageID || (parentPageID == Guid.Empty && item.ParentPageID == null))
                    {
                        cms_Pages page = new cms_Pages();
                        page.PageID = item.PageID;
                        page.PageName = item.PageName;
                        page.PageOrder = item.PageOrder;
                        page.PageCode = item.PageCode;
                        page.ParentPageID = item.ParentPageID;
                        page.FullPageUrl = RAMHX.CMS.Web.SiteContext.Pages.First(p => p.PageID == item.PageID).FullPageUrl;
                        page.FullItemPath = RAMHX.CMS.Web.SiteContext.Pages.First(p => p.PageID == item.PageID).FullItemPath;
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
            var childrens = dataContext.cms_Pages.Where(id => id.ParentPageID == pageID).Count();
            if (childrens > 0)
            {
                return false;
            }
            try
            {
                var page = dataContext.cms_Pages.Where(id => id.PageID == pageID).FirstOrDefault();
                dataContext.cms_PageHTMLModules.RemoveRange(dataContext.cms_PageHTMLModules.Where(pm => pm.PageID == pageID));
                dataContext.cms_PageFieldValues.RemoveRange(dataContext.cms_PageFieldValues.Where(pfv => pfv.PageId == pageID));

                var pts = page.cms_Templates.ToList();
                foreach (var item in pts)
                {
                    page.cms_Templates.Remove(item);
                }

                dataContext.cms_Pages.Remove(page);
                dataContext.SaveChanges();
                PageRepository.RefreshPages();
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
            var page = dataContext.cms_Pages.Where(id => id.ParentPageID == pageID).ToList();
            dataContext.cms_Pages.RemoveRange(page);
            dataContext.SaveChanges();

            DeletePage(pageID);
            PageRepository.RefreshPages();
        }

        /// <summary>
        /// Move node
        /// </summary>
        /// <param name="pageid">page id</param>
        /// <param name="ParentID">parent page id</param>
        public void MoveNode(Guid pageid, Guid ParentID, Guid oldparent, int position)
        {
            var pages = dataContext.cms_Pages.Where(id => id.PageID == pageid).FirstOrDefault();
            pages.ParentPageID = ParentID;
            if (pages.ParentPageID == oldparent)
            {
                pages.ParentPageID = oldparent;
                pages.PageOrder = position - 1;
            }
            dataContext.SaveChanges();

            PageRepository.RefreshPages();
        }

        public static List<cms_Pages> RefreshPages()
        {
            RAMHX.CMS.Web.SiteContext.Pages = new List<cms_Pages>();
            PageRepository pageRepo = new PageRepository();
            var page = pageRepo.GetPage(MainRootPageId);
            page.FullPageUrl = "/";
            page.FullItemPath = "/";
            RAMHX.CMS.Web.SiteContext.Pages.Add(page);
            foreach (var subpage in page.cms_SubPages)
            {
                AddPageToCache(subpage);
            }
            return RAMHX.CMS.Web.SiteContext.Pages;
        }

        private static void AddPageToCache(cms_Pages page)
        {
            page.FullPageUrl = "/" + page.PageUrl;
            page.FullItemPath = "/" + GetParsedPath(page.PageName.ToLower());

            if (page.ParentPageID != Guid.Empty && page.ParentPageID != null)
            {
                var pp = RAMHX.CMS.Web.SiteContext.Pages.FirstOrDefault(p => p.PageID == page.ParentPageID);
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

            RAMHX.CMS.Web.SiteContext.Pages.Add(page);

            foreach (var subpage in page.cms_SubPages)
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
            var pageRoles = dataContext.cms_PageRoles.Where(pg => pg.PageId == pageid && pg.RoleId != "").ToList();
            if (pageRoles.Count == 0 || SiteContext.CurrentUser_IsAdmin)
            {
                return true;
            }

            if (!string.IsNullOrEmpty(id))
            {
                //var userRoles = dataContext.AspNetUsers.FirstOrDefault(u => u.Id == id).AspNetRoles.ToList();
                var userRoles = SiteContext.CurrentUser_Roles;
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

        public static cms_Pages GetPageByUrl(string requestPath)
        {
            requestPath = HttpContext.Current.Server.UrlDecode(requestPath).Split('?').GetValue(0).ToString().ToLower();
            foreach (var page in RAMHX.CMS.Web.SiteContext.Pages)
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
            var pagenotfoundConf = (new AppConfiguration()).GetGroupItemByItemAndGroupID(Convert.ToInt32(Infra.Enums.AppConfigs.PageNotFoundItem.ItemId), Convert.ToInt32(Infra.Enums.AppConfigs.PageNotFoundItem.GroupId));
            if (pagenotfoundConf != null)
            {
                var pageNotFound = Repository.PageRepository.GetPageByUrl(pagenotfoundConf.ItemDesc);
                if (pageNotFound != null)
                {
                    return pageNotFound.FullPageUrl;
                }
            }
            return "/Admin/Pages/PageNotFound";
        }

        public void SaveTemplateData(System.Web.Mvc.FormCollection frm, string ids)
        {
            var idSplited = ids.Split('_');
            Guid pageID = Infra.Common.GetGuidValue(idSplited[1]);
            Guid TemplateId = Infra.Common.GetGuidValue(idSplited[2]);
            Guid TemplateFieldId = Infra.Common.GetGuidValue(idSplited[3]);
            int FieldTtypeID = Infra.Common.GetIntValue(idSplited[4]);
            if (pageID != Guid.Empty && TemplateId != Guid.Empty && TemplateFieldId != Guid.Empty)
            {
                var fieldRecord = dataContext.cms_PageFieldValues.FirstOrDefault(fl => fl.PageId == pageID && fl.TemplateId == TemplateId && fl.TemplateFieldId == TemplateFieldId);
                if (fieldRecord != null)
                {
                    fieldRecord.FieldValue = frm[ids];
                    dataContext.SaveChanges();
                }
                else
                {
                    cms_PageFieldValues pageFlVal = new cms_PageFieldValues();
                    pageFlVal.PageId = pageID;
                    pageFlVal.TemplateId = TemplateId;
                    pageFlVal.TemplateFieldId = TemplateFieldId;
                    pageFlVal.FieldValue = frm[ids];
                    dataContext.cms_PageFieldValues.Add(pageFlVal);
                    dataContext.SaveChanges();
                }
            }
        }

        public HtmlModule GetHtmlModule(string code)
        {
            return (from hm in dataContext.HtmlModules
                    where hm.HtmlModuleCode == code
                    select hm).FirstOrDefault();
        }
    }
}
