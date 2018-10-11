using RAMHX.CMS.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Repository
{
    public class RenderHtmlRepository : BaseRepository
    {
        public string HtmlOut { get; set; }

        public string GetContent(string htmlModuleCode, ControllerContext context, bool second = false)
        {
            var mainHtml = this.dataContext.HtmlModules.FirstOrDefault(h => h.HtmlModuleCode == htmlModuleCode);
            if (mainHtml != null)
            {
                var data = CacheRepository.Get(mainHtml.HTMLModuleId.ToString(), CacheTypes.HtmlModule);
                if (data != null)
                {
                    return data.ToString();
                }

            }
            bool addToCache = true;
            string html = string.Empty;
            try
            {
                if (mainHtml != null && !string.IsNullOrEmpty(mainHtml.HtmlModuleHTML))
                {
                    if (!second)
                    {
                        HtmlOut = mainHtml.HtmlModuleHTML;
                    }
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    if (!string.IsNullOrEmpty(mainHtml.HtmlModuleHTML))
                    {
                        doc.LoadHtml(mainHtml.HtmlModuleHTML);//TODO; changed by Regular expression
                    }

                    var items = doc.DocumentNode.SelectNodes("//ramhx");
                    if (items != null && items.Count > 0)
                    {
                        foreach (var item in items)
                        {
                            string htmlcd = string.Empty;
                            string viewPath = string.Empty;
                            try
                            {
                                if (item.Attributes["htmlcode"] != null && !string.IsNullOrEmpty(item.Attributes["htmlcode"].Value))
                                {
                                    htmlcd = item.Attributes["htmlcode"].Value;
                                }
                                else if (item.Attributes["viewpath"] != null && !string.IsNullOrEmpty(item.Attributes["viewpath"].Value))
                                {
                                    viewPath = item.Attributes["viewpath"].Value;
                                }

                                if (!string.IsNullOrEmpty(htmlcd) && htmlcd != htmlModuleCode)
                                {
                                    var sbhtml = this.dataContext.HtmlModules.FirstOrDefault(h => h.HtmlModuleCode == htmlcd);
                                    if (sbhtml != null)
                                    {
                                        HtmlOut = HtmlOut.Replace(item.OuterHtml, sbhtml.HtmlModuleHTML);
                                        this.GetContent(sbhtml.HtmlModuleCode, context, true);
                                    }
                                    else
                                    {
                                        HtmlOut = HtmlOut.Replace(item.OuterHtml, string.Empty);
                                        logger.Warn("Html Code -'" + htmlcd + "' does not exist.");
                                    }
                                    addToCache = false;
                                }
                                else if (!string.IsNullOrEmpty(viewPath))
                                {
                                    if (viewPath.Contains(".cshtml"))
                                    {
                                        doc.LoadHtml(RenderViewToString(context, viewPath));
                                    }
                                    else
                                    {
                                        doc.Load(HttpContext.Current.Server.MapPath(viewPath));
                                    }

                                    HtmlOut = HtmlOut.Replace(item.OuterHtml, doc.DocumentNode.OuterHtml);
                                    addToCache = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Html Code items- Error occured ", ex);
                                addToCache = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Html Code - Error occured ", ex);
                addToCache = false;
                return string.Empty;
            }

            if (addToCache)
            {
                if (!string.IsNullOrEmpty(HtmlOut))
                {
                    CacheRepository.Add(mainHtml.HTMLModuleId.ToString(), HtmlOut, CacheTypes.HtmlModule);
                    return HtmlOut;
                }
                else
                {
                    if (mainHtml != null && !string.IsNullOrEmpty(mainHtml.HtmlModuleHTML))
                    {
                        CacheRepository.Add(mainHtml.HTMLModuleId.ToString(), HtmlOut, CacheTypes.HtmlModule);
                        return mainHtml.HtmlModuleHTML;
                    }
                }

                CacheRepository.Add(mainHtml.HTMLModuleId.ToString(), HtmlOut, CacheTypes.HtmlModule);
            }

            return HtmlOut;
        }


        //public string GetViewinHtml(string viewName)
        //{
        //    var controller = ViewRenderer.CreateController<GenericController>();

        //    string html = ViewRenderer.RenderPartialView(
        //                                "~/views/shared/Error.cshtml",
        //                                model,
        //                                controller.ControllerContext);

        //    HttpContext.Current.Server.ClearError();


        //    ViewData.Model = model;

        //    using (var sw = new StringWriter())
        //    {
        //        var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
        //                                                                 viewName);
        //        var viewContext = new ViewContext(ControllerContext, viewResult.View,
        //                                     ViewData, TempData, sw);
        //        viewResult.View.Render(viewContext, sw);
        //        viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
        //        return sw.GetStringBuilder().ToString();
        //    }
        //}

        public static String RenderViewToString(ControllerContext context, String viewPath, object model = null)
        {
            if (context.Controller == null)
            {
                var ctrlFactory = ControllerBuilder.Current.GetControllerFactory();
                var ctrl = ctrlFactory.CreateController(HttpContext.Current.Request.RequestContext, HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString()) as Controller;
                context = new ControllerContext(HttpContext.Current.Request.RequestContext, ctrl);
            }

            context.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(context, viewPath, null);
                var viewContext = new ViewContext(context, viewResult.View, context.Controller.ViewData, context.Controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(context, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public List<HtmlModule> GetAllHtmlModules()
        {
            return this.dataContext.HtmlModules.ToList();
        }
    }
}
