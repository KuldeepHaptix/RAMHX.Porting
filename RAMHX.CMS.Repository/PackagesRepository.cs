using RAMHX.CMS.DataAccess;
using RAMHX.CMS.Infra;
using RAMHX.CMS.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace RAMHX.CMS.Repository
{
    public class PackagesRepository : BaseRepository
    {
        private const string fileName = "data.xml";

        public IEnumerable<string> StatusList()
        {
            var test = dataContext.cms_PackageInstallations.Select(x => x.Status).Distinct();
            return test;
        }

        public List<cms_PackageInstallations> InstallListing(string status)
        {
            var results = dataContext.cms_PackageInstallations.ToList();
            if (!string.IsNullOrEmpty(status))
            {
                results = dataContext.cms_PackageInstallations.Where(x => x.Status == status).ToList();
            }
            foreach (var item in results)
            {
                item.PackagePath = System.IO.Path.GetFileName(HttpContext.Current.Server.MapPath(item.PackagePath));
            }
            return results;
        }

        public string Export(List<Guid> pages, List<Guid> htmlModules, List<Guid> templateFields)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("", (SqlConnection)this.dataContext.Database.Connection);
            DataSet exportds = new DataSet("ramhx");

            if (htmlModules.Count() > 0)
            {
                adapter.SelectCommand.CommandText = "SELECT * FROM [HtmlModule] WHERE [HTMLModuleId] IN ('" + string.Join("','", htmlModules) + "');";
                DataTable dtHtmlModules = new DataTable("HtmlModules");
                adapter.Fill(dtHtmlModules);
                exportds.Tables.Add(dtHtmlModules);
            }

            if (templateFields.Count() > 0)
            {
                adapter.SelectCommand.CommandText = "SELECT * FROM [cms_Templates] WHERE [TemplateId] IN (SELECT TemplateId FROM cms_TemplateFields WHERE TemplateFieldId IN ('" + string.Join("','", templateFields) + "'));";
                DataTable dtcms_Templates = new DataTable("cms_Templates");
                adapter.Fill(dtcms_Templates);
                exportds.Tables.Add(dtcms_Templates);

                adapter.SelectCommand.CommandText = "SELECT * FROM cms_TemplateFields WHERE TemplateFieldId IN ('" + string.Join("','", templateFields) + "');";
                DataTable dtcms_TemplateFields = new DataTable("cms_TemplateFields");
                adapter.Fill(dtcms_TemplateFields);
                exportds.Tables.Add(dtcms_TemplateFields);
            }

            if (pages.Count() > 0)
            {
                adapter.SelectCommand.CommandText = "SELECT * FROM [cms_Pages] WHERE PageID IN ('" + string.Join("','", pages) + "');";
                DataTable dtcms_Pages = new DataTable("cms_Pages");
                adapter.Fill(dtcms_Pages);
                dtcms_Pages.Columns.Add(new DataColumn("FullItemPath"));
                foreach (DataRow page in dtcms_Pages.Rows)
                {
                    var p1 = SiteContext.Pages.First(p => p.PageID == Guid.Parse(page["PageID"].ToString()));
                    page["FullItemPath"] = p1.FullItemPath;
                }

                DataView dv = dtcms_Pages.DefaultView;
                dv.Sort = "FullItemPath ASC";
                DataTable sortedDT = dv.ToTable("cms_Pages");

                exportds.Tables.Add(sortedDT);

                adapter.SelectCommand.CommandText = "SELECT * FROM cms_PageTemplate WHERE PageID IN ('" + string.Join("','", pages) + "');";
                DataTable cms_PageTemplate = new DataTable("cms_PageTemplate");
                adapter.Fill(cms_PageTemplate);
                exportds.Tables.Add(cms_PageTemplate);

                adapter.SelectCommand.CommandText = "SELECT * FROM cms_PageFieldValues WHERE PageID IN ('" + string.Join("','", pages) + "');";
                DataTable cms_PageFieldValues = new DataTable("cms_PageFieldValues");
                adapter.Fill(cms_PageFieldValues);
                exportds.Tables.Add(cms_PageFieldValues);

                adapter.SelectCommand.CommandText = "SELECT * FROM cms_PageHTMLModules WHERE PageID IN ('" + string.Join("','", pages) + "');";
                DataTable cms_PageHTMLModules = new DataTable("cms_PageHTMLModules");
                adapter.Fill(cms_PageHTMLModules);
                exportds.Tables.Add(cms_PageHTMLModules);

                adapter.SelectCommand.CommandText = "SELECT * FROM cms_PageRoles WHERE PageID IN ('" + string.Join("','", pages) + "');";
                DataTable cms_PageRoles = new DataTable("cms_PageRoles");
                adapter.Fill(cms_PageRoles);
                exportds.Tables.Add(cms_PageRoles);
            }

            //return exportds;

            return exportds.GetXml();
        }

        private void SaveChanges()
        {
            this.dataContext.SaveChanges();
        }

        public void DeletePackage(Guid Id)
        {
            var package = this.dataContext.cms_PackageInstallations.FirstOrDefault(pack => pack.PackageId == Id);
            if (package != null)
            {
                this.dataContext.cms_PackageInstallations.Remove(package);
                this.dataContext.SaveChanges();
            }
        }

        public List<string> Import(Guid packageId)
        {
            cms_PackageInstallations package = this.dataContext.cms_PackageInstallations.First(x => x.PackageId == packageId);
            List<string> errors = new List<string>();
            try
            {

                if (package != null)
                {
                    XmlDocument xmldoc = new XmlDocument();
                    XmlNodeList xmlnode;

                    string packagePath = HttpContext.Current.Server.MapPath(package.PackagePath);

                    if (packagePath.Contains(".zip"))
                    {
                        package.StartDate = DateTime.Now;
                        package.Status = "In Progress";
                        SaveChanges();

                        //ZipFile.ExtractToDirectory(path, path.Trim('/'));
                        //path = path.Trim('/') + "data.xml";

                        if (File.Exists(Path.GetDirectoryName(packagePath) + "\\" + fileName))
                            File.Delete(Path.GetDirectoryName(packagePath) + "\\" + fileName);

                        ZipFile.ExtractToDirectory(packagePath, Path.GetDirectoryName(packagePath));

                        // path = Path.GetDirectoryName(packagePath) + "\\data.xml";

                        xmldoc.Load(Path.GetDirectoryName(packagePath) + "\\" + fileName);
                        xmlnode = xmldoc.GetElementsByTagName("ramhx");

                        XmlNodeList htmlModuleList = xmldoc.SelectNodes("/ramhx/HtmlModules");
                        XmlNodeList cms_TemplatesList = xmldoc.SelectNodes("/ramhx/cms_Templates");
                        XmlNodeList cms_TemplateFieldsList = xmldoc.SelectNodes("/ramhx/cms_TemplateFields");

                        XmlNodeList cms_PagesList = xmldoc.SelectNodes("/ramhx/cms_Pages");
                        XmlNodeList cms_PageTemplateList = xmldoc.SelectNodes("/ramhx/cms_PageTemplate");
                        XmlNodeList cms_PageFieldValueList = xmldoc.SelectNodes("/ramhx/cms_PageFieldValues");
                        XmlNodeList cms_PageHTMLModuleList = xmldoc.SelectNodes("/ramhx/cms_PageHTMLModules");
                        XmlNodeList cms_PageRoleList = xmldoc.SelectNodes("/ramhx/cms_PageRoles");

                        List<HtmlModule> hmList = new List<HtmlModule>();
                        List<cms_Templates> ctList = new List<cms_Templates>();
                        List<cms_TemplateFields> ctfList = new List<cms_TemplateFields>();
                        List<cms_Pages> pgList = new List<cms_Pages>();

                        List<cms_PageHTMLModules> pmList = new List<cms_PageHTMLModules>();
                        List<cms_PageFieldValues> pfvList = new List<cms_PageFieldValues>();
                        List<cms_PageRoles> prList = new List<cms_PageRoles>();
                        List<PageIdTemplateId> pageTemplateList = new List<PageIdTemplateId>();

                        ConvertToEntities(htmlModuleList, cms_TemplatesList, cms_TemplateFieldsList, hmList, ctList, ctfList, cms_PagesList, pgList);

                        ConvertToEntity(cms_PageFieldValueList, pfvList);
                        ConvertToEntity(cms_PageHTMLModuleList, pmList);
                        ConvertToEntity(cms_PageRoleList, prList);
                        ConvertToEntity(cms_PageTemplateList, pageTemplateList);

                        package.TotalPages = pgList.Count;
                        package.TotalModules = pmList.Count;
                        package.TotalTemplateFields = pageTemplateList.Count;
                        SaveChanges();

                        errors = ValidateEntities(hmList, ctList, ctfList, pgList, pmList, pfvList, prList, pageTemplateList);
                        if (errors.Count == 0)
                        {
                            errors = SaveEntities(hmList, ctList, ctfList, pgList, pmList, pfvList, prList, pageTemplateList, ref package);

                            // Extrate files to route directiory 
                            //ZipFile.ExtractToDirectory(Path.GetDirectoryName(packagePath) + "\\files.zip", HttpContext.Current.Server.MapPath("/"));
                            //ZipArchive zp = ZipFile.OpenRead(Path.GetDirectoryName(packagePath) + "\\files.zip");
                            ZipArchiveExtensions.ExtractToDirectory(ZipFile.OpenRead(Path.GetDirectoryName(packagePath) + "\\files.zip"), HttpContext.Current.Server.MapPath("/"), true);
                        }
                        else
                        {
                            package.IsValidPackage = false;
                            package.ComplatedDate = DateTime.Now;
                            package.Status = "Completed";
                            package.ValidationErrors = string.Join(",", errors);
                            SaveChanges();
                        }
                    }
                    else
                    {
                        errors.Add("Invalid Package - " + package.PackagePath);
                        logger.Warn("Invalid Package - " + package.PackagePath);
                    }
                }
                else
                {
                    errors.Add("Invalid Package - " + packageId);
                    logger.Warn("Invalid Package - " + packageId);
                }

            }
            catch (Exception ex)
            {
                errors.Add("Exception occurred installing Package - " + packageId);
                logger.Error("Exception occurred installing Package - " + packageId, ex);
            }

            return errors;
        }

        private List<string> SaveEntities(List<HtmlModule> hmList, List<cms_Templates> ctList, List<cms_TemplateFields> ctfieldList, List<cms_Pages> pgList, List<cms_PageHTMLModules> pmList, List<cms_PageFieldValues> pfvList, List<cms_PageRoles> prList, List<PageIdTemplateId> pageTemplateList, ref cms_PackageInstallations packageDetail)
        {
            List<string> errors = new List<string>();

            try
            {
                SaveEntity(hmList, errors, ref packageDetail);
                SaveEntity(ctList, errors, packageDetail);
                SaveEntity(ctfieldList, errors, ref packageDetail);
                SaveEntity(pgList, errors, packageDetail);
                SaveEntity(pmList, errors, ref packageDetail);
                SaveEntity(pfvList, errors, packageDetail);
                SaveEntity(prList, errors, packageDetail);
                SaveEntity(pageTemplateList, errors, packageDetail);
                packageDetail.Status = "Completed";
                packageDetail.ComplatedDate = DateTime.Now;
                SaveChanges();
                PageRepository.RefreshPages();
            }
            catch (Exception ex)
            {
                logger.Error("Exception occurred installing SaveEntities", ex);
                errors.Add("Exception occurred installing SaveEntities, please check error log for more details");
                packageDetail.IsValidPackage = false;
                packageDetail.ComplatedDate = DateTime.Now;
                packageDetail.Status = "Completed";
                packageDetail.ValidationErrors = string.Join(",", errors);
                SaveChanges();
            }
            
            return errors;
        }

        private void SaveEntity(List<cms_Templates> cms_TemplatesList, List<string> errors, cms_PackageInstallations packageDetail)
        {
            foreach (var template in cms_TemplatesList)
            {
                try
                {
                    var existTemplate = this.dataContext.cms_Templates.Where(t => t.TemplateId == template.TemplateId).FirstOrDefault();
                    if (existTemplate != null)
                    {
                        SetPropertyValues(existTemplate, template);
                        dataContext.SaveChanges();
                    }
                    else
                    {
                        dataContext.cms_Templates.Add(template);
                        dataContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("PackagesRepository.SaveEntities - Error Occured for template Import " + template.TemplateId.ToString(), ex);
                    errors.Add(string.Format("PackagesRepository.SaveEntities - Error Occured for template Import {0}, Exception: {1}", template.TemplateId.ToString(), ex.Message));
                }

            }
        }
        private void SaveEntity(List<cms_TemplateFields> cms_TemplateFieldsList, List<string> errors, ref cms_PackageInstallations packageDetail)
        {
            packageDetail.ProcTemplateFields = 0;
            foreach (var templateField in cms_TemplateFieldsList)
            {
                try
                {
                    var existTemplateField = this.dataContext.cms_TemplateFields.Where(tf => tf.TemplateFieldId == templateField.TemplateFieldId).FirstOrDefault();
                    if (existTemplateField != null)
                    {
                        SetPropertyValues(existTemplateField, templateField);
                        packageDetail.ProcTemplateFields++;
                        packageDetail.ModifiedDate = DateTime.Now;
                        existTemplateField.cms_Templates = this.dataContext.cms_Templates.Where(tf => tf.TemplateId == templateField.TemplateId).FirstOrDefault();
                        this.dataContext.SaveChanges();
                    }
                    else
                    {
                        dataContext.cms_TemplateFields.Add(templateField);
                        packageDetail.ProcTemplateFields++;
                        packageDetail.ModifiedDate = DateTime.Now;
                        this.dataContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("PackagesRepository.SaveEntities - Error Occured for templateField Import " + templateField.TemplateFieldId.ToString(), ex);
                    errors.Add(string.Format("PackagesRepository.SaveEntities - Error Occured for templateField Import {0}, Exception: {1}", templateField.TemplateFieldId.ToString(), ex.Message));
                }
            }
        }
        private void SaveEntity(List<HtmlModule> hmList, List<string> errors, ref cms_PackageInstallations packageDetail)
        {
            foreach (var htmlModule in hmList)
            {
                try
                {
                    var existHM = this.dataContext.HtmlModules.Where(h => h.HTMLModuleId == htmlModule.HTMLModuleId).FirstOrDefault();
                    if (existHM != null)
                    {
                        SetPropertyValues(existHM, htmlModule);
                        packageDetail.ProcModules++;
                        packageDetail.ModifiedDate = DateTime.Now;
                        dataContext.SaveChanges();
                    }
                    else
                    {
                        dataContext.HtmlModules.Add(htmlModule);
                        packageDetail.ProcModules++;
                        packageDetail.ModifiedDate = DateTime.Now;
                        dataContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("PackagesRepository.SaveEntities - Error Occured for htmlModule Import " + htmlModule.HTMLModuleId.ToString(), ex);
                    errors.Add(string.Format("PackagesRepository.SaveEntities - Error Occured for htmlModule Import {0}, Exception: {1}", htmlModule.HTMLModuleId.ToString(), ex.Message));
                }

            }
        }
        private void SaveEntity(List<cms_Pages> pgList, List<string> errors, cms_PackageInstallations packageDetail)
        {
            //dataContext = new DatabaseEntities();
            packageDetail.ProcPages = 0;
            foreach (var cmsPage in pgList)
            {
                try
                {
                    var existPage = this.dataContext.cms_Pages.Where(p => p.PageID == cmsPage.PageID).FirstOrDefault();
                    if (existPage != null)
                    {
                        SetPropertyValues(existPage, cmsPage);
                        existPage.cms_ParentPage = this.dataContext.cms_Pages.Where(p => p.PageID == cmsPage.ParentPageID).FirstOrDefault();
                        packageDetail.ProcPages++;
                        packageDetail.ModifiedDate = DateTime.Now;
                        dataContext.SaveChanges();
                    }
                    else
                    {
                        dataContext.cms_Pages.Add(cmsPage);
                        packageDetail.ProcPages++;
                        packageDetail.ModifiedDate = DateTime.Now;
                        dataContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("PackagesRepository.SaveEntities - Error Occured for Pages Import " + cmsPage.PageID.ToString(), ex);
                    errors.Add(string.Format("PackagesRepository.SaveEntities - Error Occured for Pages Import {0}, Exception: {1}", cmsPage.PageID.ToString(), ex.Message));
                }
            }
        }
        private void SaveEntity(List<cms_PageHTMLModules> pmList, List<string> errors, ref cms_PackageInstallations packageDetail)
        {
            foreach (var cmsPageHTMLModule in pmList)
            {
                try
                {
                    var pageHTMLModule = this.dataContext.cms_PageHTMLModules.Where(pm => pm.PageHTMLModuleId == cmsPageHTMLModule.PageHTMLModuleId).FirstOrDefault();
                    if (pageHTMLModule != null)
                    {
                        SetPropertyValues(pageHTMLModule, cmsPageHTMLModule);
                        dataContext.SaveChanges();
                    }
                    else
                    {
                        dataContext.cms_PageHTMLModules.Add(cmsPageHTMLModule);
                        dataContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("PackagesRepository.SaveEntities - Error Occured for cms_PageHTMLModules Import " + cmsPageHTMLModule.PageHTMLModuleId.ToString(), ex);
                    errors.Add(string.Format("PackagesRepository.SaveEntities - Error Occured for Pages Import {0}, Exception: {1}", cmsPageHTMLModule.PageHTMLModuleId.ToString(), ex.Message));
                }
            }
        }
        private void SaveEntity(List<cms_PageFieldValues> pfvList, List<string> errors, cms_PackageInstallations packageDetail)
        {
            foreach (var cmsPageFieldValue in pfvList)
            {
                var tid = Guid.Empty;


                if (cmsPageFieldValue.TemplateId != null && cmsPageFieldValue.TemplateFieldId != null && cmsPageFieldValue.PageId != null)
                {
                    try
                    {
                        var p = this.dataContext.cms_Pages.FirstOrDefault(p1 => p1.PageID == cmsPageFieldValue.PageId);
                        var t = this.dataContext.cms_Templates.FirstOrDefault(p1 => p1.TemplateId == cmsPageFieldValue.TemplateId);
                        var tfv = this.dataContext.cms_TemplateFields.FirstOrDefault(p1 => p1.TemplateFieldId == cmsPageFieldValue.TemplateFieldId);
                        if (p != null && t != null && tfv != null)
                        {
                            var existPageFieldValue = this.dataContext.cms_PageFieldValues.Where(pfv => pfv.PageId == cmsPageFieldValue.PageId && pfv.TemplateId == cmsPageFieldValue.TemplateId && pfv.TemplateFieldId == cmsPageFieldValue.TemplateFieldId).FirstOrDefault();
                            if (existPageFieldValue != null)
                            {
                                SetPropertyValues(existPageFieldValue, cmsPageFieldValue);
                                dataContext.SaveChanges();
                            }
                            else
                            {
                                dataContext.cms_PageFieldValues.Add(cmsPageFieldValue);
                                dataContext.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(string.Format("PackagesRepository.SaveEntities - Error Occured for cms_PageFieldValues Import PageId {0}, TemplateId {1}, TemplateField {2}", cmsPageFieldValue.PageId, cmsPageFieldValue.TemplateId, cmsPageFieldValue.TemplateFieldId), ex);

                        errors.Add(string.Format("PackagesRepository.SaveEntities - Error Occured for cms_PageFieldValues Import PageId {0}, TemplateId {1}, TemplateField {2}, Exception: {3}", cmsPageFieldValue.PageId, cmsPageFieldValue.TemplateId, cmsPageFieldValue.TemplateFieldId, ex.Message));
                    }
                }
                else
                {
                    logger.Warn(string.Format("PackagesRepository.SaveEntities - cms_PageFieldValues Import PageId {0}, TemplateId {1}, TemplateField {2} Has NULL template Id", cmsPageFieldValue.PageId, cmsPageFieldValue.TemplateId, cmsPageFieldValue.TemplateFieldId));

                }
            }
        }
        private void SaveEntity(List<cms_PageRoles> prList, List<string> errors, cms_PackageInstallations packageDetail)
        {
            foreach (var cmsPageRoles in prList)
            {
                try
                {
                    var existPageRole = this.dataContext.cms_PageRoles.Where(pr => pr.PageId == cmsPageRoles.PageId).FirstOrDefault();
                    if (existPageRole != null)
                    {
                        SetPropertyValues(existPageRole, cmsPageRoles);
                        dataContext.SaveChanges();
                    }
                    else
                    {
                        dataContext.cms_PageRoles.Add(cmsPageRoles);
                        dataContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("PackagesRepository.SaveEntities - Error Occured for cms_PageRoles Import " + cmsPageRoles.PageId.ToString(), ex);
                    errors.Add(string.Format("PackagesRepository.SaveEntities - Error Occured for Pages Import {0}, Exception: {1}", cmsPageRoles.PageId.ToString(), ex.Message));
                }
            }
        }
        private void SaveEntity(List<PageIdTemplateId> pageTemplateList, List<string> errors, cms_PackageInstallations packageDetail)
        {
            foreach (var pageTemplate in pageTemplateList)
            {
                try
                {
                    new PageRepository().AssignTemplate(pageTemplate.PageId, pageTemplate.TemplateId);
                }
                catch (Exception ex)
                {
                    logger.Error("PackagesRepository.SaveEntities - Error Occured for cms_PageRoles Import " + pageTemplate.PageId.ToString(), ex);
                    errors.Add(string.Format("PackagesRepository.SaveEntities - Error Occured for Pages Import {0}, Exception: {1}", pageTemplate.PageId.ToString(), ex.Message));
                }
            }
        }
        private List<string> ValidateEntities(List<HtmlModule> hmList, List<cms_Templates> ctList, List<cms_TemplateFields> ctfList, List<cms_Pages> pgList, List<cms_PageHTMLModules> pmList, List<cms_PageFieldValues> pfvList, List<cms_PageRoles> prList, List<PageIdTemplateId> pageTemplateList)
        {
            List<string> errors = new List<string>();

            //foreach (var hm in hmList)
            //{
            //    Guid hmid = Guid.Empty;

            //    if (Guid.TryParse(hm.HTMLModuleId.ToString(), out hmid))
            //        errors.Add($"Invalid Html Module({hm.HTMLModuleId})");
            //}


            // Validate Template Fields
            foreach (cms_TemplateFields ctf in ctfList)
            {
                Guid tId = Guid.Empty;

                if (Guid.TryParse(ctf.TemplateId.ToString(), out tId))
                {
                    // Check for template is exist or not
                    cms_Templates pageTemp = this.dataContext.cms_Templates.FirstOrDefault(t => t.TemplateId == ctf.TemplateId) ?? ctList.FirstOrDefault(x => x.TemplateId == ctf.TemplateId);
                    if (pageTemp == null)
                        errors.Add(string.Format("Template not found for Template Field id {0}", ctf.TemplateFieldId));
                }
                else
                {
                    // Check templete id is valid or not
                    errors.Add(string.Format("Invalid Template Field Id {0}", ctf.TemplateFieldId));
                }
            }


            foreach (cms_Pages pg in pgList)
            {
                // Ignore Main page for parent page
                if (pg.PageCode != "MAIN_PAGE")
                {
                    //Check parent page is exits or not
                    cms_Pages parentPage = this.dataContext.cms_Pages.FirstOrDefault(p => p.PageID == pg.ParentPageID) ?? pgList.FirstOrDefault(x => x.PageID == pg.ParentPageID);
                    if (parentPage == null)
                        errors.Add(string.Format("Parent page not found for page id {0}", pg.PageID));
                }

                // Check for page template is exist or not
                foreach (var pt in pg.cms_Templates)
                {
                    cms_Templates pageTemp = this.dataContext.cms_Templates.FirstOrDefault(t => t.TemplateId == pt.TemplateId) ?? ctList.FirstOrDefault(x => x.TemplateId == pt.TemplateId);
                    if (pageTemp == null)
                        errors.Add(string.Format("Page template not found for page id {0} and Template id {1}", pg.PageID, pt.TemplateId));
                }

                // Get html Modules for page
                List<cms_PageHTMLModules> pageModules = this.dataContext.cms_PageHTMLModules.Where(x => x.PageID == pg.PageID).ToList();

                // Add new Modules for page
                pageModules.AddRange(pmList.Where(x => x.PageID == pg.PageID).ToList());

                // Check module is exits ot not
                foreach (var pm in pageModules)
                {
                    HtmlModule hm = this.dataContext.HtmlModules.FirstOrDefault(h => h.HTMLModuleId == pm.HTMLModuleId) ?? hmList.FirstOrDefault(x => x.HTMLModuleId == pm.HTMLModuleId);
                    if (hm == null)
                        errors.Add(string.Format("Html Module not found for Page id {0} and Module Id {}", pg.PageID, pm.HTMLModuleId));
                }
            }

            //foreach (cms_PageHTMLModules pm in pmList)
            //{
            //    cms_Pages page = this.dataContext.cms_Pages.FirstOrDefault(p => p.PageID == pm.PageID) ?? pgList.FirstOrDefault(x => x.PageID == pm.PageID);
            //    if (page == null)
            //        errors.Add($"Page not found for Page Module id {pm.PageHTMLModuleId}");

            //    HtmlModule hm = this.dataContext.HtmlModules.FirstOrDefault(h => h.HTMLModuleId == pm.HTMLModuleId) ?? hmList.FirstOrDefault(x => x.HTMLModuleId == pm.HTMLModuleId);
            //    if (hm == null)
            //        errors.Add($"Html Module not found for Page Module id {pm.PageHTMLModuleId}");
            //}

            return errors;
        }
        private void ConvertToEntities(XmlNodeList htmlModuleList, XmlNodeList cms_TemplatesList, XmlNodeList cms_TemplateFieldsList, List<HtmlModule> hmList, List<cms_Templates> ctList, List<cms_TemplateFields> ctfList, XmlNodeList cms_PagesList, List<cms_Pages> pgList)
        {
            try
            {
                foreach (XmlNode item in htmlModuleList)
                {
                    HtmlModule hm = new HtmlModule();
                    SetPropertyValues(item, hm);
                    hmList.Add(hm);
                }

                foreach (XmlNode item in cms_TemplatesList)
                {
                    cms_Templates ct = new cms_Templates();
                    SetPropertyValues(item, ct);
                    ctList.Add(ct);
                }

                foreach (XmlNode item in cms_TemplateFieldsList)
                {
                    cms_TemplateFields cft = new cms_TemplateFields();
                    SetPropertyValues(item, cft);
                    ctfList.Add(cft);
                }

                foreach (XmlNode item in cms_PagesList)
                {
                    cms_Pages cp = new cms_Pages();
                    SetPropertyValues(item, cp);
                    pgList.Add(cp);
                }

            }
            catch (Exception ex)
            {
                logger.Error("Exception occurred ConvertToEntities ", ex);
            }
        }
        private void ConvertToEntity(XmlNodeList xmlNodeList, List<cms_PageHTMLModules> pmList)
        {
            foreach (XmlNode item in xmlNodeList)
            {
                cms_PageHTMLModules pm = new cms_PageHTMLModules();
                SetPropertyValues(item, pm);
                pmList.Add(pm);
            }
        }
        private void ConvertToEntity(XmlNodeList xmlNodeList, List<cms_PageFieldValues> pfvList)
        {
            foreach (XmlNode item in xmlNodeList)
            {
                cms_PageFieldValues pfv = new cms_PageFieldValues();
                SetPropertyValues(item, pfv);
                pfvList.Add(pfv);
            }
        }
        private void ConvertToEntity(XmlNodeList xmlNodeList, List<cms_PageRoles> prList)
        {
            foreach (XmlNode item in xmlNodeList)
            {
                cms_PageRoles pr = new cms_PageRoles();
                SetPropertyValues(item, pr);
                prList.Add(pr);
            }
        }
        private void ConvertToEntity(XmlNodeList xmlNodeList, List<PageIdTemplateId> pageTemplateList)
        {
            foreach (XmlNode item in xmlNodeList)
            {
                PageIdTemplateId pt = new PageIdTemplateId();
                SetPropertyValues(item, pt);
                pageTemplateList.Add(pt);
            }
        }
        private void SetPropertyValues(XmlNode item, object ct)
        {
            try
            {
                foreach (PropertyInfo prop in ct.GetType().GetProperties())
                {
                    if (prop.PropertyType.Name == typeof(System.String).Name || (prop.PropertyType.GetGenericArguments().Length > 0 && prop.PropertyType.GetGenericArguments()[0].Name == typeof(System.String).Name))
                    {
                        if (item[prop.Name] != null)
                            prop.SetValue(ct, item[prop.Name].InnerText);
                    }
                    else if (prop.PropertyType.Name == typeof(System.DateTime).Name || (prop.PropertyType.GetGenericArguments().Length > 0 && prop.PropertyType.GetGenericArguments()[0].Name == typeof(System.DateTime).Name))
                    {
                        if (item[prop.Name] != null)
                            prop.SetValue(ct, Convert.ChangeType(item[prop.Name].InnerText, prop.PropertyType.GetGenericArguments()[0]));
                    }
                    else if (prop.PropertyType.Name == typeof(System.Guid).Name || (prop.PropertyType.GetGenericArguments().Length > 0 && prop.PropertyType.GetGenericArguments()[0].Name == typeof(System.Guid).Name))
                    {

                        if (item[prop.Name] != null)
                            prop.SetValue(ct, Guid.Parse(item[prop.Name].InnerText));
                    }
                    else if (prop.PropertyType.Name == typeof(System.Int32).Name || (prop.PropertyType.GetGenericArguments().Length > 0 && prop.PropertyType.GetGenericArguments()[0].Name == typeof(System.Int32).Name))
                    {
                        if (item[prop.Name] != null)
                            prop.SetValue(ct, Convert.ToInt32(item[prop.Name].InnerText));
                    }


                    //if (item[prop.Name] != null)
                    //    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    //    {
                    //        if (!String.IsNullOrEmpty(item[prop.Name].InnerText))
                    //            prop.SetValue(ct, Convert.ChangeType(item[prop.Name].InnerText, prop.PropertyType.GetGenericArguments()[0]));
                    //    }
                    //    else
                    //    {
                    //        prop.SetValue(ct, Convert.ChangeType(item[prop.Name].InnerText, prop.PropertyType));
                    //    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Exception occurred ConvertToEntities ", ex);
            }
        }
        private static void SetPropertyValues(object dest, object source)
        {
            foreach (PropertyInfo prop in source.GetType().GetProperties())
            {
                prop.SetValue(dest, prop.GetValue(source));
            }
        }
        public Guid InstallRequest(byte[] package, string packageName, string requestedUserName)
        {
            cms_PackageInstallations tblpackaheRepo = new cms_PackageInstallations();
            string fileNameExtension = System.IO.Path.GetExtension(packageName);

            if (fileNameExtension.ToLower() == ".zip")
            {
                try
                {
                    string webconfigkey = new AppConfiguration().GetGroupItemByItemAndGroupID(Common.Constants.AppConfigItemCodes.RAMHXPackageInstallPath, Convert.ToInt32(Enums.AppConfigs.PackageInstallPath.AppSettings)).ItemDesc;
                    tblpackaheRepo.PackageId = Guid.NewGuid();
                    string packageLogPath = (webconfigkey + "/" + tblpackaheRepo.PackageId).Replace("//", "/");

                    string folderPath = HttpContext.Current.Server.MapPath(packageLogPath);

                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }

                    var pf = File.Create(folderPath + "/" + packageName);
                    pf.Close();

                    BinaryWriter Writer = null;
                    // Create a new stream to write to the file
                    Writer = new BinaryWriter(File.OpenWrite(folderPath + "/" + packageName));
                    // Writer raw data                
                    Writer.Write(package);
                    Writer.Flush();
                    Writer.Close();

                    cms_PackageInstallations saveFile = new cms_PackageInstallations();
                    saveFile.PackageId = tblpackaheRepo.PackageId;
                    saveFile.PackagePath = packageLogPath + "/" + packageName;
                    saveFile.CreatedDate = DateTime.Now;
                    saveFile.CreatedBy = requestedUserName;
                    saveFile.Status = "Pending";
                    //saveFile.StartDate = Convert.ToDateTime("10/12/2016");
                    //saveFile.ModifiedDate = Convert.ToDateTime("10/12/2016");
                    //saveFile.ComplatedDate = Convert.ToDateTime("10/12/2016");
                    //saveFile.TotalPages = Convert.ToInt32("1");
                    //saveFile.TotalModules = Convert.ToInt32("1");
                    //saveFile.TotalTemplateFields = Convert.ToInt32("1");
                    saveFile.ProcPages = 0;
                    saveFile.ProcModules = 0;
                    saveFile.ProcTemplateFields = 0;
                    saveFile.IsValidPackage = true;
                    saveFile.ValidationErrors = "";

                    dataContext.cms_PackageInstallations.Add(saveFile);
                    dataContext.SaveChanges();

                    return tblpackaheRepo.PackageId;
                    // Import(saveFile.PackagePath);
                    // PageRepository.RefreshPages();

                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {

                        logger.Warn(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            logger.Warn(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    throw;
                }
            }
            else
            {
                logger.Warn("requested package is invalid - package: " + packageName);
            }

            return Guid.Empty;
        }
        public Guid InstallRequest(HttpPostedFileBase package, string requestedUserName)
        {
            byte[] b = null;
            using (MemoryStream ms = new MemoryStream())
            {
                int count = 0;
                do
                {
                    byte[] buf = new byte[1024];
                    count = package.InputStream.Read(buf, 0, 1024);
                    ms.Write(buf, 0, count);
                } while (package.InputStream.CanRead && count > 0);
                b = ms.ToArray();
            }
            return this.InstallRequest(b, package.FileName, requestedUserName);

            //cms_PackageInstallations tblpackaheRepo = new cms_PackageInstallations();
            //string fileNameExtension = System.IO.Path.GetExtension(package.FileName);

            //if (fileNameExtension.ToLower() == ".zip")
            //{
            //    try
            //    {
            //        string webconfigkey = new AppConfiguration().GetGroupItemByItemAndGroupID(Common.Constants.AppConfigItemCodes.RAMHXPackageInstallPath, Convert.ToInt32(Enums.AppConfigs.PackageInstallPath.AppSettings)).ItemDesc;
            //        tblpackaheRepo.PackageId = Guid.NewGuid();
            //        string packageLogPath = (webconfigkey + "/" + tblpackaheRepo.PackageId).Replace("//", "/");

            //        string folderPath = HttpContext.Current.Server.MapPath(packageLogPath);

            //        if (!System.IO.Directory.Exists(folderPath))
            //        {
            //            System.IO.Directory.CreateDirectory(folderPath);
            //        }
            //        using (var fileStream = File.Create(folderPath + "/" + package.FileName))
            //        {
            //            package.InputStream.Seek(0, SeekOrigin.Begin);
            //            package.InputStream.CopyTo(fileStream);
            //        }

            //        cms_PackageInstallations saveFile = new cms_PackageInstallations();
            //        saveFile.PackageId = tblpackaheRepo.PackageId;
            //        saveFile.PackagePath = packageLogPath + "/" + package.FileName;
            //        saveFile.CreatedDate = DateTime.Now;
            //        saveFile.CreatedBy = requestedUserName;
            //        saveFile.Status = "Pending";
            //        //saveFile.StartDate = Convert.ToDateTime("10/12/2016");
            //        //saveFile.ModifiedDate = Convert.ToDateTime("10/12/2016");
            //        //saveFile.ComplatedDate = Convert.ToDateTime("10/12/2016");
            //        //saveFile.TotalPages = Convert.ToInt32("1");
            //        //saveFile.TotalModules = Convert.ToInt32("1");
            //        //saveFile.TotalTemplateFields = Convert.ToInt32("1");
            //        saveFile.ProcPages = 0;
            //        saveFile.ProcModules = 0;
            //        saveFile.ProcTemplateFields = 0;
            //        saveFile.IsValidPackage = true;
            //        saveFile.ValidationErrors = "";

            //        dataContext.cms_PackageInstallations.Add(saveFile);
            //        dataContext.SaveChanges();

            //        // Import(saveFile.PackagePath);
            //        // PageRepository.RefreshPages();

            //    }
            //    catch (DbEntityValidationException e)
            //    {
            //        foreach (var eve in e.EntityValidationErrors)
            //        {

            //            logger.Warn(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //                eve.Entry.Entity.GetType().Name, eve.Entry.State));
            //            foreach (var ve in eve.ValidationErrors)
            //            {
            //                logger.Warn(string.Format("- Property: \"{0}\", Error: \"{1}\"",
            //                    ve.PropertyName, ve.ErrorMessage));
            //            }
            //        }
            //        throw;
            //    }
            //}
            //else
            //{
            //    logger.Warn("requested package is invalid - package: " + package.FileName);
            //}
        }
        private class PageIdTemplateId
        {
            public Guid PageId { get; private set; }
            public Guid TemplateId { get; private set; }
        }
    }
}
